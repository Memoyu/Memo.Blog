const path = require('path');

module.exports = {
  webpack: {
    // 配置路径别名
    alias: {
      '@': path.join(__dirname, 'src')
    }
    // rules: [
    //   {
    //     test: /\.s[ac]ss$/i,
    //     use: [
    //       // 将 JS 字符串生成为 style 节点
    //       'style-loader',
    //       // 将 CSS 转化成 CommonJS 模块
    //       'css-loader',
    //       // 将 Sass 编译成 CSS
    //       'sass-loader'
    //     ]
    //   }
    // ]
  }
};

export {};
