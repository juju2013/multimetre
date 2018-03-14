\ interface to ads1115
\ uses i2c

[ifndef] ADS.ADDR  $48 constant ADS.ADDR  [then] \ default address


\ : ads-init ( -- ) \ init ads
\   i2c-init
\   ADS.ADDR i2c-addr
\   $01 >i2c
\   \ $62 >i2c $03 >i2c 0 i2c-xfer drop \ AIN2 - GND, 4.096V and 8 SPS
\   $e0 >i2c $03 >i2c 0 i2c-xfer drop \ AIN2 - GND, 6V and 8 SPS
\   \ $60 >i2c $13 >i2c 0 i2c-xfer drop \ AIN2 - GND, 6V, win comparator and 8 SPS
\ ;

: ads-read ( -- n ) \ read the ADC value
  ADS.ADDR i2c-addr
  $00 >i2c
  2 i2c-xfer drop i2c> i2c> swap 8 lshift +
;

: ads.v ( -- v ) \ read the voltage in mV
  ADS.ADDR i2c-addr
  $01 >i2c
  $e0 >i2c $03 >i2c 0 i2c-xfer drop \ AIN2 - GND, 6V and 8 SPS
  300 ms
  6144 ads-read $7FFF */
;

: ads.r ( -- v ) \ read the voltage in mA
  ADS.ADDR i2c-addr
  $01 >i2c
  $0E >i2c $03 >i2c 0 i2c-xfer drop \ AIN0 - AN1, 0.256V and 8 SPS
  300 ms
  2560 ads-read $7FFF */
;

task: multimeter

: multimeter& ( -- ) \ run the volmeter
  multimeter activate
  i2c-init lcd-init clear display 
  begin 
    ads.v ads.r
    clear lcd.setlo shownum lcd.sethi shownum showaxis display
  again
;
