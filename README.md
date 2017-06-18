# ZoomingUsingBilinearInterpolation

In mathematics, bilinear interpolation is an extension of linear interpolation for interpolating functions of two variables (e.g., x and y) on a rectilinear 2D grid.

The key idea is to perform linear interpolation first in one direction, and then again in the other direction. Although each step is linear in the sampled values and in the position, the interpolation as a whole is not linear but rather quadratic in the sample location.

More about bilinear interpolation is available in: https://en.wikipedia.org/wiki/Bilinear_interpolation

This software is a Windows Console Application, example of usage:

C:\>ZoomInterpolacao.exe param1 param2 param3

param1 is the input image

param2 is the zooming factor

param3 is the output image

C:\>ZoomInterpolacao.exe lena.jpg 2 lena_zoom.jpg


Thanks to https://www.theengineeringprojects.com/2016/02/image-zooming-bilinear-interpolation-matlab.html
