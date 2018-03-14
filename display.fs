\ redefin display routing from embello/.../flib/digits.fs

: lcd.hirow ( n -- n ) \ convert index J into lcd y coordinate in inversed display
  64 swap -
;


: lcd.lorow ( n -- n ) \ convert index J into lcd y coordinate in inversed display
  32 swap -
;

' lcd.hirow variable 'lcd.row

: lcd.sethi ['] lcd.hirow 'lcd.row ! ;
: lcd.setlo ['] lcd.lorow 'lcd.row ! ;

: lcd.row 'lcd.row @ execute ;

: showdigit ( n x -- )
  swap 120 * digits + 30 0 do
    30 0 do
      i $1F xor bit over bit@ if over 128 swap - i - j lcd.row putpixel then
    loop
    4 +
  loop 2drop ;

: shownum ( u -- )
  10 /mod 10 /mod 10 /mod
  0 showdigit 30 showdigit 60 showdigit 90 showdigit
;

: showaxis ( -- ) \  display axis
  axis 61 1 do
    dup c@ 8 0 do 
      dup i bit and if
        i 64 j - putpixel
      then
    loop drop
    1+
  loop drop
;
