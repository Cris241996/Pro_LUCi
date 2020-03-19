using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using BarcodeLib.BarcodeReader;

namespace ManualV2
{
        public partial class Form1 : Form
        {
            public Form1()
            {
                InitializeComponent();
            }
            /**************
             * 
            LEER CODIGOS QR CON WEBCAM
             * 
             * importar libreria barcodelib
            **************/
            //AGREGAR USING*/
            //VARIABLE PARA LISTA DE DISPOSITIVOS
            private FilterInfoCollection Dispositivos;
            //VARIABLE PARA FUENTE DE VIDEO
            private VideoCaptureDevice FuenteDeVideo;
            private void Form1_Load_1(object sender, EventArgs e)
            {
                //LISTAR DISPOSITIVOS DE ENTRADA DE VIDEO
                Dispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                //CARGAR TODOS LOS DISPOSITIVOS AL COMBO
                foreach (FilterInfo x in Dispositivos)
                {
                    comboBox1.Items.Add(x.Name);
                }
                comboBox1.SelectedIndex = 0;
            }
            
            private void button1_Click_1(object sender, EventArgs e)
        {
                //EMPEZAR A LEER
                timer1.Enabled = true;

                //ESTABLECER EL DISPOSITIVO SELECCIONADO COMO FUENTE DE VIDEO
                FuenteDeVideo = new VideoCaptureDevice(Dispositivos[comboBox1.SelectedIndex].MonikerString);
                //INICIALIZAR EL CONTROL
                videoSourcePlayer1.VideoSource = FuenteDeVideo;
                //INICIAR RECEPCION DE IMAGENES
                videoSourcePlayer1.Start();
            }

            private void button2_Click_1(object sender, EventArgs e)
            {
                //DEJAR DE LEER
                timer1.Enabled = false;


                //DETENER RECEPCION DE IMAGENES
                videoSourcePlayer1.SignalToStop();
            }

            private void timer1_Tick(object sender, EventArgs e)
            {
                //ESTAR SEGUROS QUE HAY UNA IMAGEN DESDE LA WEBCAM
                if (videoSourcePlayer1.GetCurrentVideoFrame() != null)
                {
                    //IBTENER IMAGEN DE LA WEBCAM
                    Bitmap img = new Bitmap(videoSourcePlayer1.GetCurrentVideoFrame());
                    //UTILIZAR LA LIBRERIA Y LEER EL CÓDIGO
                    string[] resultados = BarcodeReader.read(img, BarcodeReader.QRCODE);
                    //QUITAR LA IMAGEN DE MEMORIA
                    img.Dispose();
                    //OBTENER LAS LECTURAS CUANDO SE LEA ALGO
                    if (resultados != null && resultados.Count() > 0)
                    {
                        //AGREGAR EL TEXTO OBTENIDO A LA LISTA
                        if (resultados[0].IndexOf("1111") != -1)
                        {
                            //QUITAR EL CODIGO DE VERIFICACION
                            resultados[0] = resultados[0].Replace("1111", "");
                            listBox1.Items.Add(resultados[0]);
                        }
                    }
                }
            }

      
    }
    }