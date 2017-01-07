@echo off
Assoc .teng=tengfile
Ftype tengfile="%~dp0gui.exe" %%1