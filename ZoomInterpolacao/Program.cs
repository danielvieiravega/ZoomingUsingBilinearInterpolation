using System;
using System.Drawing;

/*
 * Disciplina: Métodos Computacionais EC
 * Aluno: Daniel Vieira Vega - 2017/1
 */

namespace ZoomUsingInterpolation
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Erro! Informe o caminho completo da imagem, fator de escalamento e o nome que deseja salvar a imagem ampliada.");
                Console.WriteLine(@"Exemplo: programa.exe c:\tmp\minha_imagem.jpg 2 c:\tmp\minha_imagem_ampliada.jpg");
            }
            else
            {
                var imagePath = args[0];
                //try
                //{
                var factor = int.Parse(args[1]);
                var image = (Bitmap)Image.FromFile(imagePath, true);

                var y = image.Height;
                var x = image.Width;

                //Criando a imagem vazia
                var zoomedImage = new Bitmap(x * factor, y * factor);
                for (var i = 0; i < zoomedImage.Width; i++)
                {
                    for (var j = 0; j < zoomedImage.Height; j++)
                    {
                        zoomedImage.SetPixel(i, j, System.Drawing.Color.Yellow);
                    }
                }


                var counterWidth = 0;
                for (var i = 0; i < zoomedImage.Width; i += factor)
                {
                    var counterHeight = 0;
                    for (var j = 0; j < zoomedImage.Height; j += factor)
                    {
                        zoomedImage.SetPixel(i, j, image.GetPixel(counterWidth, counterHeight));
                        counterHeight++;
                    }
                    counterWidth++;
                }

                var heigth = (y) * factor;
                var width = (x) * factor;

                var total = width * heigth;

                for (decimal i = 0; i < width; i++)
                {
                    for (decimal j = 0; j < heigth; j++)
                    {
                        if (((i) % factor == 0) && ((j) % factor == 0))
                        {
                            //
                        }
                        else
                        {
                            
                            if (i == 11 && j == 2)
                            {
                                var xgfd = 34;
                            }

                            if (i == 0 && j == 1)
                            {
                                var xgfd = 34;
                            }

                            var ceil_i_scale_factor = (int)Math.Ceiling(i / factor) * factor;
                            var ceil_j_scale_factor = (int)Math.Ceiling(j / factor) * factor;

                            var h00x = ceil_i_scale_factor - factor + 1;
                            var h00y = ceil_j_scale_factor - factor + 1;
                            //h00=im1( ceil(i/fac)*fac-fac+1,       ceil(j/fac)*fac-fac+1,:); 
                            //var NW_x = i == 0 ? ceil_i_scale : ceil_i_scale - factor;
                            var NW_x = ceil_i_scale_factor - factor;
                            if (NW_x < 0)
                                NW_x = 0;
                            //var NW_y = j == 0 ? ceil_j_scale : ceil_j_scale - factor;
                            var NW_y = ceil_j_scale_factor - factor;
                            if (NW_y < 0)
                                NW_y = 0;

                            var h10x = ceil_i_scale_factor - factor + 1 + factor;
                            var h10y = ceil_j_scale_factor - factor + 1;
                            //h10=im1( ceil(i/fac)*fac-fac+1+fac,       ceil(j/fac)*fac-fac+1,:);
                            //var SW_x = i == 0 ? ceil_i_scale + factor : ceil_i_scale;
                            var SW_x = ceil_i_scale_factor;// + factor;
                            if (SW_x < 0)
                                SW_x = 0;
                            //if (SW_x == width)
                            //    SW_x--;
                            if (SW_x >= width)
                                SW_x = width - 1;

                                //var SW_y = j == 0 ? ceil_j_scale : ceil_j_scale - factor;
                                var SW_y = ceil_j_scale_factor - factor;
                            if (SW_y < 0)
                                SW_y = 0;

                            var h01x = ceil_i_scale_factor - factor + 1;
                            var h01y = ceil_j_scale_factor - factor + 1 + factor;
                            //h01=im1( ceil(i/fac)*fac-fac+1,       ceil(j/fac)*fac-fac+1+fac,:);
                            //var NE_x = i == 0 ? ceil_i_scale : ceil_i_scale - factor;
                            var NE_x = ceil_i_scale_factor - factor;
                            if (NE_x < 0)
                                NE_x = 0;
                            //var NE_y = j == (heigth - 1) ? ceil_j_scale_factor : ceil_j_scale_factor + factor;
                            var NE_y = ceil_j_scale_factor;
                            if (NE_y >= heigth)
                                NE_y = heigth - 1;
                            //if (NE_y == heigth)
                            //    NE_y--;

                            //var NE_y = i == 0 ? ceil_j_scale * scale : ceil_j_scale * scale + scale;

                            var h11x = ceil_i_scale_factor - factor + 1 + factor;
                            var h11y = ceil_j_scale_factor - factor + 1 + factor;
                            //h11=im1( ceil(i/fac)*fac-fac+1+fac,       ceil(j/fac)*fac-fac+1+fac,:);
                            var SE_x = i == 0 ? ceil_i_scale_factor + factor : ceil_i_scale_factor;
                            //if (SE_x == width)
                            //    SE_x--;
                            if (SE_x >= width)
                                SE_x = width - 1;

                                //var SE_y = j == (heigth - 1) ? ceil_j_scale_factor : ceil_j_scale_factor + factor;
                                var SE_y = ceil_j_scale_factor;
                            if (SE_y >= heigth)
                                SE_y = heigth - 1;

                            var pixel_NW = zoomedImage.GetPixel(NW_x, NW_y);
                            var pixel_SW = zoomedImage.GetPixel(SW_x, SW_y);
                            var pixel_NE = zoomedImage.GetPixel(NE_x, NE_y);
                            var pixel_SE = zoomedImage.GetPixel(SE_x, SE_y);

                            var x_toBeCalculated = (i) % factor;
                            var y_toBeCalculated = (j) % factor;

                            var dx = x_toBeCalculated / factor;
                            var dy = y_toBeCalculated / factor;

                            var pixel_NW_media_color = System.Windows.Media.Color.FromArgb(pixel_NW.A, pixel_NW.R, pixel_NW.G, pixel_NW.B);
                            var pixel_SW_media_color = System.Windows.Media.Color.FromArgb(pixel_SW.A, pixel_SW.R, pixel_SW.G, pixel_SW.B);
                            var pixel_NE_media_color = System.Windows.Media.Color.FromArgb(pixel_NE.A, pixel_NE.R, pixel_NE.G, pixel_NE.B);
                            var pixel_SE_media_color = System.Windows.Media.Color.FromArgb(pixel_SE.A, pixel_SE.R, pixel_SE.G, pixel_SE.B);

                            var b1 = pixel_NW_media_color;
                            var b2 = System.Windows.Media.Color.Subtract(pixel_SW_media_color, pixel_NW_media_color);
                            var b3 = System.Windows.Media.Color.Subtract(pixel_NE_media_color, pixel_NW_media_color);

                            //pixel_NE + pixel_SE
                            var pixel_NE_pixel_SE = System.Windows.Media.Color.Add(pixel_NE_media_color, pixel_SE_media_color);
                            //pixel_SW - pixel_NE - pixel_SE;
                            var pixel_SW_pixel_NE_pixel_SE = System.Windows.Media.Color.Subtract(pixel_SW_media_color, pixel_NE_pixel_SE);
                            //pixel_NW - pixel_SW - pixel_NE + pixel_SE;

                            var b4 = System.Windows.Media.Color.Subtract(pixel_NW_media_color, pixel_SW_pixel_NE_pixel_SE);

                            //b4*dx*dy
                            var b4_dx_dy = System.Windows.Media.Color.Multiply(b4, (float)dx * (float)dy);

                            //b3*dy
                            var b3_dy = System.Windows.Media.Color.Multiply(b3, (float)dy);

                            //b2*dx
                            var b2_dx = System.Windows.Media.Color.Multiply(b2, (float)dx);

                            //b3*dy + b4*dx*dy
                            var b3_dy_b4_dx_dy = System.Windows.Media.Color.Add(b3_dy, b4_dx_dy);

                            //b2*dx + b3*dy + b4*dx*dy
                            var b2_dx_b3_dy_b4_dx_dy = System.Windows.Media.Color.Add(b2_dx, b3_dy_b4_dx_dy);

                            //b1 + b2*dx + b3*dy + b4*dx*dy
                            var value = System.Windows.Media.Color.Add(b1, b2_dx_b3_dy_b4_dx_dy);
                            var value_drawing_color = System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B);

                            zoomedImage.SetPixel((int)i, (int)j, value_drawing_color);
                        }
                    }
                }

                zoomedImage.Save(args[2], System.Drawing.Imaging.ImageFormat.Jpeg);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Imagem ampliada com sucesso! Pressione qualquer tecla para sair.");
                //}
                //catch (Exception e)
                //{
                //    Console.ForegroundColor = ConsoleColor.Red;
                //    Console.WriteLine("Houve uma falha durante o processamento da imagem.");
                //    Console.WriteLine(e.Message);
                //}
            }

            Console.ReadLine();
        }
    }
}
