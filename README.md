#草稿，这都是草稿，包括源代码和README.md都是草稿

**存在撞墙、弃坑的可能(99%)**

**瞎写 反正要重构**

**代码瞎眼请轻喷**

**想一起搞大新闻欢迎邮件或者Issue**

**个人能力有限 考虑不周 如有错误和疑问 欢迎指正**


#目标功能

脑洞太多懒得写 大概就是个基于4层隧道（UDP）的VPN吧

**可能描述不明确，也许就这就是一个 隧道 或者 某种代理 。**

我把他定义成VPN，因为实现目标确实是一个虚拟网络，具有寻址功能。

不考虑二层 三层数据交换的需求，这的确是个**只面向UDP和TCP服务**的VPN

VPN层之上：扔掉IP协议，自己写寻址，或者直接协商新的专用隧道，最终只面向TCP和UDP服务（可能以后增加）

VPN层之下：UDP + 可选的黑科技（可靠 + 优化 + P2P + 混淆 + etc.）

etc.（只要有脑洞）

行了别脑洞那么多了，能正常使用再说吧。




#当前代码简要说明

认证（目前无认证）->必要的信息交换（打算先写死）->传输数据（正在写）

认证和数据先分开端口 开两个socket

先睡觉去了

认证包 

数据包 