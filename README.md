1.前提:win10以上系统,安装vs2022(.net 6.0)和mysql 8.0.13
(请自行安装,去微软官网和mysql官网)

2.完整的项目工程目录如下：

![image](https://github.com/janper168/FaitionFzp/assets/29268530/7bb64f1d-cee8-48cf-bd55-8929ed9cf958)

其中，
CodeGenerator                                  //后端代码生成工具
JFK.BLL                                        //业务逻辑层代码
JKF.ChatApi                                    //即时通讯系统webapi服务
JKF.DB                                         //数据服务层代码
JKF.FrameWork.sln                              //解决方案vs
JKF.Utils                                      //公用设施
JKF.Web                                        //网站
mysql db init                                  //mysql数据库初始化脚本
Others
代码片段注入                           

3.vs工程代码片段的注入,请按说明.txt去操作,这样代码片段注入功能才能生效

![image](https://github.com/janper168/FaitionFzp/assets/29268530/015ed5a3-ffbf-4ca9-a935-5cb76f212028)

4.执行工具命令行:
update-database
****![image](https://github.com/janper168/FaitionFzp/assets/29268530/2290e30d-896e-409b-8a27-14d2494b8c1f)
![image](https://github.com/janper168/FaitionFzp/assets/29268530/78c2cf9d-03d3-4e5b-80f9-7edb01b7c9b5)

我这里是因为执行过一遍了,它告诉我数据库已经是最新了.

4.导入mysql初始数据, 主要是菜单模块 区域 和 超级管理员账号

执行:new.sql

![image](https://github.com/janper168/FaitionFzp/assets/29268530/053fcaa3-61dd-4121-8db8-8bed62b0b81e)


5.从vs工程启动JFK.Web项目:

![image](https://github.com/janper168/FaitionFzp/assets/29268530/c0276fa7-0712-45ed-a039-256486a7f33d)
![image](https://github.com/janper168/FaitionFzp/assets/29268530/98cc968f-cb3b-4377-94f8-eab6a950119f)


可以联系作者本人（wx：fang-zhanpeng,qq:363553360） 搭建环境.


B站有该项目的介绍：

https://www.bilibili.com/video/BV1Ej421X7re?t=4.5
https://www.bilibili.com/video/BV1ku4y1F7k7?t=375.0

