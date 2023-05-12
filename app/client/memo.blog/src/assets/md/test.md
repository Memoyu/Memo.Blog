**本文主要记录在船新的 CentOS 8 中安装配置.NET Core 运行环境以及配合使用的 Redis，Redis 的安装配置相对比较简单，综合了网上的教程进行实践，并最终完成配置正常使用。废话不多说，开始！**

## 一、安装 Redis

##### 1、安装

```
yum install redis
```

##### 2、查询本次安装的版本（主要用于查看 Redis 所有文件位置）

```
rpm -qa|grep redis
```

`结果：redis-5.0.3-1.module_el8.0.0+6+ab019c03.x86_64`

##### 3、查询安装位置

```
rpm -ql redis-5.0.3-1.module_el8.0.0+6+ab019c03.x86_64
```

![文件目录](https://img2020.cnblogs.com/blog/1667295/202007/1667295-20200704123645169-1397302012.png)

##### 4、开启 Redis Server

```
cd /usr/bin
```

```
redis-server
```

![开启服务](https://img2020.cnblogs.com/blog/1667295/202007/1667295-20200704123701712-11895092.png)

> 至此，Redis Server 已经可以启动了！

##### 5、客户端连接 Redis Server

开启一个新的 Linux 连接窗口。此处为连接本地的 Redis，故无需带上 ip 以及端口参数

```
cd /usr/bin
```

```
redis-cli
```

![开启客户端](https://img2020.cnblogs.com/blog/1667295/202007/1667295-20200704123740976-1862745613.png)

> 本地客户端连接也是正常的！

## 二、远程连接 Redis

### 1、配置

完成以上步骤，仅仅是在本地连接上能够使用，一旦使用外部调用，则无法使用。此处外部设备操作系统为 win10，其他的根据自身情况度娘设置。<font color=#A52A2A size=4 >其次，在外部连接之前，先配置阿里云 ESC 安全组配置，添加 Redis 端口安全组，默认端口为 6379</font>

#### 问题 ①：在不进行配置的情况下，直接连接远程端 Redis Server 会报如下错误：

```
(error) DENIED Redis is running in protected mode because protected mode is enabled, no bind address was specified,
no authentication password is requested to clients. In this mode connections are only accepted from the loopback
interface. If you want to connect from external computers to Redis you may adopt one of the following solutions: 1)
 Just disable protected mode sending the command 'CONFIG SET protected-mode no' from the loopback interface by
connecting to Redis from the same host the server is running, however MAKE SURE Redis is not publicly accessible
from internet if you do so. Use CONFIG REWRITE to make this change permanent. 2) Alternatively you can just disable
the protected mode by editing the Redis configuration file, and setting the protected mode option to 'no', and then
 restarting the server. 3) If you started the server manually just for testing, restart it with the '--protected-mode
 no' option. 4) Setup a bind address or an authentication password. NOTE: You only need to do one of the above things
 in order for the server to start accepting connections from the outside.
```

> 此处大致意思为：配置中 protected-mode 属性为 yes，即当前处于保护模式下，无法连接。我们需要设置其为 no

#### 问题 ②：无法操作远程 Redis，进行 Redis 操作报错:

```
Error: 在驱动器 %1 上插入软盘。
```

> 此处问题进行密码设置即可。

#### 以上问题的解决方法

在<font color=#A52A2A >本地客户端连接</font>情况下，进行如下操作：

##### a、打开 Redis 配置文件 Redis.conf

```
cd /etc
```

```
vim redis.conf
```

<font color=#A52A2A >**`按下键盘i进入编辑模式`**</font>

找到如下参数，并修改：

**针对问题 ① 的修改：**

```
bind 127.0.0.1  	 //行数：69   =》修改为#bind 127.0.0.1
```

```
protected-mode yes      //行数：89  =》修改为protected-mode no
```

```
daemonize yes 	        //行数：137  =》修改为daemonize no //设置为no则不作为后台运行，否则后台运行
```

![问题①设置示例](https://img2020.cnblogs.com/blog/1667295/202007/1667295-20200704123828610-1475218193.png)

**针对问题 ② 的修改：**

```
# requirepass foobared   //行数：508  =》在508行下添加与行，内容为requirepass code6076..
```

![问题②设置示例](https://img2020.cnblogs.com/blog/1667295/202007/1667295-20200704123843570-1526317419.png)

##### b、保存配置文件并退出

<font color=#A52A2A >**`按下键盘esc退出编辑模式，然后按shift + : 键，再录入wq,回车即可。`**</font>

![](https://img2020.cnblogs.com/blog/1667295/202007/1667295-20200704123935368-43365893.png)

##### c、重新启动 Redis Server

```
redis-server /etc/redis.conf
```

<font color=#A52A2A >**`此处进行Redis服务器启动并指定配置文件，以应用我们改的配置。`**</font>

**注：**如果上面设置 daemonize 参数

为 no 时，回车后，效果如下：（为空白，实则已经非后台执行，当前控制台已被占用）

![daemonize no](https://img2020.cnblogs.com/blog/1667295/202007/1667295-20200704123952461-1140377008.png)

为 yes 时：回车后，效果如下：（后台执行，可继续操作）

![daemonize yes](https://img2020.cnblogs.com/blog/1667295/202007/1667295-20200704124016646-705726197.png)

### 2、连接

以 Windows 10 为例，连接方式如下：

```
D:\WorkSpace\Redis\redis-cli -h x.x.x.x -p 6379 -a code6076..
```

![连接测试](https://img2020.cnblogs.com/blog/1667295/202007/1667295-20200704124043190-25646475.png)

**大功告成！**
