"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe template\template.sln /property:Config=Debug+Release+Linux"

rm -R .\release\

mkdir release
cd release
mkdir Windows
cd Windows
mkdir TengDK
robocopy ..\..\template\gui\bin\Release TengDK\ /E /XF "*.xml" "*.config" "*.pdb" "*vshost*"
robocopy ..\..\template\template\bin\Release TengDK\ /E /XF "*.xml" "*.config" "*.pdb" "*vshost*"
mkdir "Teng Sample"
xcopy /s /e /y "..\..\sample" ".\Teng Sample\"
cd ..\
Compress-Archive "Windows\*" "TengDK Windows.zip"

mkdir Linux
cd Linux
mkdir TengDK
robocopy ..\..\template\gui\bin\Linux TengDK\ /E /XF "*.xml" "*.config" "*.pdb" "*vshost*" "*.bat"
robocopy ..\..\template\template\bin\Linux TengDK\ /E /XF "*.xml" "*.config" "*.pdb" "*vshost*" "*.bat"
mkdir "Teng Sample"
xcopy /s /e /y "..\..\sample" ".\Teng Sample\"
cd ..\
Compress-Archive "Linux\*" "TengDK Linux.zip"

pause