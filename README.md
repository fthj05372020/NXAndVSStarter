# NXAndVSStarter
基于.NET的多个版本的NX 和VS 启动器

本项目环境： VS2012+ WIN10 64

目的：通过一个软件以操作版的形式管理多个版本的 seimens NX 以及 VS 的启动，
并可以设置 NX 的userdir 环境变量指向特定的文件夹，以便加载特定的NX的插件/外挂包，方便用户进行多个版本的NX的二次开发和测试。

Release文件夹下的 config.txt 用于设置多个版本的 NX/VS的安装目录，userdirs.txt 用于设置插件包的路径，允许多个，首个为默认项
