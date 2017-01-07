@echo off
Assoc .teng=tengfile
Ftype tengfile="%~dp0teng.exe" %%1