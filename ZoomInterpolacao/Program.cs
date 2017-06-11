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
                try
                {
                    var scale = int.Parse(args[1]);
                    var image = (Bitmap)Image.FromFile(imagePath, true);

                    var y = image.Height;
                    var x = image.Width;

                    var newImage = new Bitmap(x * scale, y * scale);

                    for (var i = 0; i < newImage.Width; i++)
                    {
                        for (var j = 0; j < newImage.Height; j++)
                        {
                            newImage.SetPixel(i, j, System.Drawing.Color.Black);
                        }
                    }

                    var counterWidth = 0;
                    for (var i = 0; i < newImage.Width; i += scale)
                    {
                        var counterHeight = 0;
                        for (var j = 0; j < newImage.Height; j += scale)
                        {
                            newImage.SetPixel(i, j, image.GetPixel(counterWidth, counterHeight));
                            counterHeight++;
                        }
                        counterWidth++;
                    }

                    var heigth = (y - 2) * scale;
                    var width = (x - 2) * scale;

                    var total = width * heigth;

                    for (decimal i = 0; i < width; i++)
                    {
                        for (decimal j = 0; j < heigth; j++)
                        {
                            if (((i) % scale == 0) && ((j) % scale == 0))
                            {
                                //
                            }
                            else
                            {
                                var ceil_i_scale = (int)Math.Ceiling(i / scale);
                                var ceil_j_scale = (int)Math.Ceiling(j / scale);

                                var NW_x = i == 0 ? ceil_i_scale * scale : ceil_i_scale * scale - scale;
                                var NW_y = i == 0 ? ceil_j_scale * scale - scale : ceil_j_scale * scale;

                                var SW_x = i == 0 ? ceil_i_scale * scale + scale : ceil_i_scale * scale;
                                var SW_y = i == 0 ? ceil_j_scale * scale - scale : ceil_j_scale * scale;

                                var NE_x = i == 0 ? ceil_i_scale * scale : ceil_i_scale * scale - scale;
                                var NE_y = i == 0 ? ceil_j_scale * scale : ceil_j_scale * scale + scale;

                                var SE_x = i == 0 ? ceil_i_scale * scale + scale : ceil_i_scale * scale;
                                var SE_y = i == 0 ? ceil_j_scale * scale : ceil_j_scale * scale + scale;

                                var pixel_NW = newImage.GetPixel(NW_x, NW_y);
                                var pixel_SW = newImage.GetPixel(SW_x, SW_y);
                                var pixel_NE = newImage.GetPixel(NE_x, NE_y);
                                var pixel_SE = newImage.GetPixel(SE_x, SE_y);

                                var x_toBeCalculated = (i) % scale;
                                var y_toBeCalculated = (j) % scale;

                                var dx = x_toBeCalculated / scale;
                                var dy = y_toBeCalculated / scale;

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

                                newImage.SetPixel((int)i, (int)j, value_drawing_color);
                            }
                        }
                    }

                    newImage.Save(args[2], System.Drawing.Imaging.ImageFormat.Jpeg);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Imagem ampliada com sucesso! Pressione qualquer tecla para sair.");
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Houve uma falha durante o processamento da imagem.");
                    Console.WriteLine(e.Message);
                }
            }
            
            Console.ReadLine();
        }
    }
}
