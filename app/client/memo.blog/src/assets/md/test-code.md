**关键词：uni-app；ECharts；**

认知尚浅，如有高见，愿闻其详。

&ensp;

**&emsp;&emsp;前言：**本次的教程与上次的[基于 WePY 2.x 平台下使用 ECharts](https://www.cnblogs.com/memoyu/p/14360278.html)方式基本一致，毕竟目标平台都是微信小程序而已（别的平台未测试），只是就是多了一个参数而已。

&emsp;&emsp;本次使用的是仍然是[echarts-for-weixin](https://github.com/ecomfe/echarts-for-weixin)组件，其对小程序进行了兼容适配，我们进行直接下载项目的组件文件，然后引入就能使用了。其次，还有一种方式，就是去 uni-app 插件市场进行 echarts 搜索，是有人已经做了适配的，同样是在此项目的基础上改的，只不过他的方式是通过 ec 参数传入 options，进行数据赋值。废话不多说，开干。

&ensp;

## 步骤

1. 先下载开源项目[echarts-for-weixin](https://github.com/ecomfe/echarts-for-weixin)，把开源项目中 ec-canvas 文件夹中的文件拷到自己的项目中**（千万别下 Release，好像还是老版本....）**

![](https://img2020.cnblogs.com/blog/1667295/202102/1667295-20210202121022694-60271341.png)

2. 对引入组件中的`ec-canvas.js`文件进行一点点的修改（**重要**）

   > 本步骤主要是为了解决引入后使用中点击无效果与 echart 初始化问题，具体问题于文章底部详述。

![](https://img2020.cnblogs.com/blog/1667295/202103/1667295-20210318180740068-1014450640.png)

3. 在需要在`pages.json`下的`globalStyle`引入`ec-canvas`组件

   > ①-全局引入`ec-canvas`组件，用于承载统计图表

   ```json
    "globalStyle": {
     ...
     "usingComponents": {//引入全局ec-canvas组件
     	"ec-canvas": "/static/plugin/echart/ec-canvas"//根据自己放的路径改变
     }
    },
   ```

4. 进行`Page`或`Component`下的` template`节点构建以及`style`样式引入，

   > ①-构建节点

   ```vue
   <template>
     <view class="container">
       <view class="main">
         <ec-canvas
           id="month-trend-bar-dom"
           class="month-trend"
           canvas-id="month-trend-bar"
           @init="echartBarInit"
           :ec="ec"
         >
         </ec-canvas>
       </view>
     </view>
   </template>
   ```

   > ②-引入样式&emsp;&emsp;&emsp; **注意修改`lang`，本教程中使用的是`scss`**

   ```css
   <style lang="scss">
   .container{
     margin-top: 30px;
     min-height: 100%;
     .main{
       width: 750rpx;
       .month-trend{
         display: block;
         width: 750rpx;
         height: 500rpx;
       }
     }
   }
   </style>
   ```

5. 声明`data`中的`ec`以及定义`echartBarInit`初始化方法

   > ①-声明 `ec`

   ```js
   data: {
       // 有需要的可进行一些配置
       ec: {
       }
   },
   ```

   > ②-定义`echarts`初始胡方法`echartBarInit`

   ```js
   methods: {
       echartBarInit({ detail }) {
         // 初始化方法
         this.initChart(
           detail.echart,//ec-canvas传回的echart参数
           detail.canvas,
           detail.width,
           detail.height,
           detail.dpr,
           detail.wxNode//ec-canvas传回的this
         );
       },
       initChart(echart, canvas, width, height, dpr, wxNode) {
         let chart = echart.init(canvas, null, {//进行chart初始化
           width: width,
           height: height,
           devicePixelRatio: dpr,
         });
         canvas.setChart(chart);
         var option = {
           color: [
             "#37A2DA",
             "#32C5E9",
             "#67E0E3",
             "#91F2DE",
             "#FFDB5C",
             "#FF9F7F",
           ],
           legend: {
             data: ["直接访问", "邮件营销", "联盟广告"],
           },
           xAxis: [
             {
               type: "category",
               data: ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"],
               axisTick: {
                 alignWithLabel: true,
               },
             },
           ],
           yAxis: [
             {
               type: "value",
             },
           ],
           series: [
             {
               name: "直接访问",
               type: "bar",
               barWidth: "60%",
               data: [10, 52, 200, 334, 390, 330, 220],
             },
             {
               name: "邮件营销",
               type: "bar",
               stack: "总量",
               label: {
                 normal: {
                   show: true,
                   position: "insideRight",
                 },
               },
               data: [120, 132, 101, 134, 90, 230, 210],
             },
             {
               name: "联盟广告",
               type: "bar",
               stack: "总量",
               label: {
                 normal: {
                   show: true,
                   position: "insideRight",
                 },
               },
               data: [220, 182, 191, 234, 290, 330, 310],
             },
           ],
         };
         chart.setOption(option);//赋值option
         wxNode.chart = chart;//赋值ec-canvas中的chart参数，达到监听效果实现
       },
     }
   ```

**至此，整个教程已经结束了，不出问题，你就可以看到效果了。**
**完整代码请移步至我的开源项目：**[Memoyu/mbill_wechat_app: 基于 uni-app 搭建个人记账小程序](https://github.com/Memoyu/mbill_wechat_app)

&ensp;

## 点击无效果问题

请参考此文底部：[WePY 2.x 下使用 ECharts - Memoyu - 博客园 (cnblogs.com)](https://www.cnblogs.com/memoyu/p/14360278.html)

&ensp;

## 效果

![](https://img2020.cnblogs.com/blog/1667295/202103/1667295-20210318180539315-1020042920.gif)
