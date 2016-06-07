# HiCSAop
C#对AOP支持不是很好,使用ProxyAttribute,则要求类必须继承自ContextBoundObject,而且只能添加到类上,很不方便,且网上很多的AOP实现起来很麻烦,所以实现该类库,该类库使用VS2013开发,
除了提供ProxyAttribute实现外,还提供基于Castle DynamicProxy的实现.

思考:
1: 如果在对象创建,方法调用的时候,使用IsDefined判断是否添加了某些定制特性,应该会非常好用,且有利于AOP的实现,不知道有没有这样的知识点.
2: 使用Castle DynamicProxy的StandardInterceptor有如下限制:
1) 使用ProxyGenerator创建对象,而不是new对象
2) 添加AOP的函数必须是虚函数.
3) 还是不能将AOP加到方法上
4) 无法应用多个AOP(可能这是一个伪命题)
5) 我期望的是用定制特性实现,而不是使用不一样的new对象

解决问题:
1) 不需要继承自ContextBoundObject

3: 听说Autofac对Castle DynamicProxy进行了进一步封装,下一步实验一下这个库.
