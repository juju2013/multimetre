#! /usr/bin/env python
# -*- coding: utf-8 -*-

from PIL import Image

def gennumber(n):
  fname = "%s.bmp"%n
  with Image.open(fname) as im:
    sz = im.size
    x, y = sz
    for i in range(y):
      l=""
      for j in range(x):
        (r,g,b, a) = im.getpixel((j,i))
        l+= "0" if (r+g+b) != 0 else "1"
      print("%s ,"%l)
    print("")
  
def main():
  #print("create digits")
  #print("  binary")
  #for i in range(10):
  #  gennumber(i)
  #print("  decimal")
  gennumber("axis3")

if __name__=="__main__":
  main()
