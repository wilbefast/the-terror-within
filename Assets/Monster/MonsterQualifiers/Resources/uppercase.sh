#!/bin/sh

for folder in Arms Legs Tail Heads
do
	for file in $folder/*.png
	do
		base=$(basename $file) 
		uppercase=$(tr '[a-z]' '[A-Z]'<<<"${base:0:1}")
		out=$folder/${uppercase}${base:1}
		mv $file $out
	done
done