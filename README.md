# Pocket Weather For Windows Mobile 5.x/6.x & Windows Phone 7.x
声明一下，本人并非大佬，如果发现本项目有问题的话可以提出来！
这是一个支持Windows Mobile 5.x/6.x & Windows Phone 7.x的天气客户端！目前WM端已在SGH i908l和HP IPAQ 110手机上通过测试！WP端仍在测试中...
网站：http://www.winvistasp.top/weather.php

## 大致原理
其实非常简单，就是搭个Web Service，当客户端连接时返回对应的天气信息

## 使用教程（WM客户端）
1.下载bin_1_015.exe
2.拷贝到wm手机上
3.运行！（没错，就这么简单！）

当然，你也可以在设置里面修改你想要查看的城市
如果你想要使用自己搭建的服务器，也可以在设置里面修改

## 使用教程（WP客户端）
还没开发完，先留个空位

## 使用教程（服务器端）
1.下载bin_s_007.zip
2.部署到IIS上即可

如果你想用其他的天气服务，可以将setphp.txt的内容改为对应的API提供的xml地址；
因为服务器端使用较老的.NET版本编译，可能无法正常获取xml文件，这时你可以使用weather.php，将php文件内的$url变量改为xml地址，再将setphp.txt的内容改为“[weather.php的url链接]?loc=”，例如“http://192.168.1.3/weather.php?loc=”；
data.log是保存服务器日志的，你可以在这个文件里查看服务器保存的日志。

## 源码
请使用Visual Studio 2008 SP1打开本项目！
