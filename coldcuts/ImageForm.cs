﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Un4seen.Bass;

namespace ColdCutsNS
{
    public partial class ImageForm : Form
    {
        Pen m_penGreen = new Pen(Color.LimeGreen, 1);
        Pen m_penRed = new Pen(Color.Red, 1);
        Pen m_penBlue = new Pen(Color.Blue, 1);

        Font m_font = new Font("Arial", 7, FontStyle.Regular);
        Brush m_brush = new SolidBrush(Color.Black);

        PictureBox m_soundwavePictureBox;
        List<int> m_volumeSamples;

        double m_maxVolume = 0;
        const int m_waveHeight = 130;

        const int m_redMarkerModifier = 50;
        int m_resolutionScale = 1;
        MainForm m_parent;
        List<int> markers = new List<int>();

        public ImageForm(MainForm parent)
        {
            InitializeComponent();
            m_parent = parent;
            Width = parent.Width;
        }

        public void ShowSound(List<int> volumeSamples)
        {
            m_volumeSamples = volumeSamples;
            int sampleCount = m_volumeSamples.Count;

            for (int i = 0; i < sampleCount; i++)
            {
                if (m_volumeSamples[i] > m_maxVolume)
                {
                    m_maxVolume = m_volumeSamples[i];
                }
            }
            RedrawSound(sampleCount);
        }

        public void UpdateDrawSound(OutputFiles soundFiles)
        {
            markers.Clear();
            var files = soundFiles.OrderBy(x => x.startTimeSeconds);
            foreach (var file in files)
            {
                if (file.endTimeSeconds > 0)
                {
                    markers.Add(TimeToPoint(file.endTimeSeconds));
                }
            }
            markers.Sort();
            RedrawSound(m_volumeSamples.Count);
        }

        private void RedrawSound(int sampleCount)
        {
            panel.Controls.Clear();

            List<int> adjustedSamples = new List<int>();

            for(int i = 0; i < m_volumeSamples.Count; i++)
            {
                if(i % m_resolutionScale == 0)
                {
                    adjustedSamples.Add(m_volumeSamples[i]);
                }
            }

            //if we have too many samples we need to split them up to properly display large sound files
            // max width determined by 64bit int
            int totalDisplaySamples = sampleCount / m_resolutionScale;
            List<Bitmap> bitmaps = new List<Bitmap>();

            if (totalDisplaySamples > 65535)
            {

                int numberOfBitmaps = (int)Math.Ceiling(totalDisplaySamples / 65535f);

                for (int i = 0; i < numberOfBitmaps; i++)
                {
                    //we don't want to just add max width bitmaps for the number of bitmaps we need
                    if(i == numberOfBitmaps - 1)
                    {
                        //last bitmap in the list needs to be trimmed down
                        int remainingPixels = totalDisplaySamples % 65535;
                        Bitmap lastBitmap = new Bitmap(remainingPixels, m_waveHeight);
                        bitmaps.Add(lastBitmap);
                    }
                    else
                    {
                        bitmaps.Add(new Bitmap(65535, m_waveHeight));
                    }
                }
            }
            else
            {
                bitmaps.Add(new Bitmap(totalDisplaySamples, m_waveHeight));
            }

            int sampleLocation = 0;

            for (int j = 0; j < bitmaps.Count; j++)
            {
                int currentImageSamples = bitmaps[j].Width;

                using (Graphics graphics = Graphics.FromImage(bitmaps[j]))
                {
                    for (int i = 1; i < currentImageSamples; i++)
                    {

                        int lineHeight = m_waveHeight - (int)Math.Round((adjustedSamples[sampleLocation] / m_maxVolume) * m_waveHeight);

                        graphics.DrawLine(m_penGreen, i, m_waveHeight, i, lineHeight);

                        if (i % m_redMarkerModifier == 0)
                        {
                            graphics.DrawLine(m_penRed, i, 0, i, 10);

                            PointF timeStampPoint = new PointF(i, 0);
                            string timeStamp = ((sampleLocation + 1) * m_resolutionScale / 3000).ToString() + ":" + string.Format("{0:00}", ((sampleLocation + 1)  * m_resolutionScale/ m_redMarkerModifier) % 60);

                            graphics.DrawString(timeStamp, m_font, m_brush, timeStampPoint);

                            if (i % (m_redMarkerModifier * 10) == 0)
                            {
                                graphics.DrawLine(m_penRed, i - 1, 0, i - 1, 6);
                                graphics.DrawLine(m_penRed, i - 2, 0, i - 2, 2);
                            }
                        }

                        sampleLocation++;
                    }

                    for (int i = 0; i < markers.Count; i++)
                    {
                        var x = markers[i] / m_resolutionScale;
                        graphics.DrawLine(m_penBlue, x, 0, x, m_waveHeight);
                    }
                }

                Point currentImagePosition = new Point(0, m_waveHeight * j);

                m_soundwavePictureBox = new SoundPicture(bitmaps[j], currentImageSamples, m_waveHeight, currentImagePosition);
                m_soundwavePictureBox.MouseClick += WaveFormPictureBoxClicked;

                panel.Controls.Add(m_soundwavePictureBox);
            }
        }

        private double PointToTime(Point p)
        {
            return PointToTime(p.X);
        }

        private double PointToTime(int x)
        {
            return x * m_resolutionScale / (double)m_redMarkerModifier;
        }

        private int TimeToPoint(double time)
        {
            return (int)Math.Truncate(time * m_redMarkerModifier / m_resolutionScale);
        }

        private void PlayAt(Point p)
        {
            int stream = Bass.BASS_StreamCreateFile(m_parent.Mp3File, 0, 0, BASSFlag.BASS_SAMPLE_MONO);
            Bass.BASS_ChannelSetPosition(stream, seconds: PointToTime(p));
            Bass.BASS_ChannelPlay(stream, false);
            Timer timer = new Timer { Interval = 5000 };
            timer.Tick += (s, e) => { Bass.BASS_ChannelStop(stream); };
            timer.Start();
        }

        private void AddMarker(int x)
        {
            Bitmap bitmap = (Bitmap)m_soundwavePictureBox.Image;
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawLine(m_penBlue, x, 0, x, bitmap.Height);
            }
            panel.Refresh();
            RefreshMarkers(x);
        }

        private void RefreshMarkers(int x)
        {
            markers.Add(x);
            markers.Sort();

            m_parent.outputFiles.RemoveAllSoundFiles();
            m_parent.dataGridView1.Rows.Clear();
            m_parent.Refresh();

            var soundFile = new SoundFile();
            m_parent.addSoundFile(soundFile);
            for (int i = 0; i < markers.Count; i++)
            {
                soundFile = new SoundFile
                {
                    fileName = $"File_{i}",
                    startTimeSeconds = (i == 0) ? 0 : PointToTime(markers[i - 1]),
                    endTimeSeconds = PointToTime(markers[i])
                };
                m_parent.addSoundFile(soundFile);
            }
            soundFile = new SoundFile
            {
                fileName = $"File_{markers.Count}",
                startTimeSeconds = PointToTime(markers[markers.Count-1]),
                endTimeSeconds = PointToTime(m_volumeSamples.Count)
            };
            m_parent.addSoundFile(soundFile);
        }

        public void DecreaseSoundwaveResolution(object sender, EventArgs e)
        {
            decreaseResolutionButton.Enabled = true;
            m_resolutionScale++;
            if(m_resolutionScale >= 3)
            {
                m_resolutionScale = 3;
                increaseResolutionButton.Enabled = false;
            }
            RedrawSound(m_volumeSamples.Count);
        }

        public void IncreaseSoundwaveResolution(object sender, EventArgs e)
        {
            increaseResolutionButton.Enabled = true;
            m_resolutionScale--;
            if(m_resolutionScale < 1)
            {
                m_resolutionScale = 1;
                decreaseResolutionButton.Enabled = false;
            }
            RedrawSound(m_volumeSamples.Count);
        }

        private void ImageForm_DoubleClick(object sender, EventArgs e)
        {
            Location = new Point(m_parent.Location.X, m_parent.Location.Y + m_parent.Height);
        }

        public bool IsNearParent
        {
            get
            {
                return (Math.Abs(Location.X - m_parent.Location.X) < 20) &&
                (Math.Abs(Location.Y - (m_parent.Location.Y + m_parent.Height)) < 20);
            }
        }

        public void StickToParent()
        {
            Location = new Point(m_parent.Location.X, m_parent.Location.Y + m_parent.Height);
        }

        private void ImageForm_LocationChanged(object sender, EventArgs e)
        {
            if (IsNearParent) StickToParent();
        }

        private Point Click_Position;
        private void WaveFormPictureBoxClicked(Object sender, MouseEventArgs e)
        {
            Click_Position = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Right)
            {
                contextMenu.Show(Cursor.Position);
            }
            else
            {
                AddMarker(Click_Position.X);
            }
        }

        private void AddMenuItem_Click(object sender, EventArgs e)
        {
            AddMarker(Click_Position.X);
        }

        private void PlayMenuItem_Click(object sender, EventArgs e)
        {
            PlayAt(Click_Position);
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
