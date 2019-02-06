var debug = process.env.NODE_ENV !== "production";
var codeVersion = process.env.CODE_VERSION;
var webpack = require('webpack');
var path = require('path');

console.log("DEBUG=", debug);
console.log("ENV=", process.env.NODE_ENV);


if (debug) {
  codeVersion = "dev";
}

console.log("CODE VERSION = ", codeVersion);

module.exports = {
  context: path.join(__dirname, "src"),
  devtool: debug ? "inline-sourcemap" : false,
  entry: "./js/client.tsx",
  module: {
    loaders: [
      {
        test: /\.jsx?$/,
        exclude: /(node_modules|bower_components)/,
        loader: 'babel-loader',
        query: {
          presets: ['react', 'es2015', 'stage-0'],
          plugins: ['react-html-attrs', 'transform-class-properties', 'transform-decorators-legacy'],
        }
      },
      {
        test: /\.tsx?$/,
        enforce: 'pre',
        exclude: /(node_modules|bower_components|typings)/,
        loader: 'tslint-loader',
      },
      {
        test: /\.tsx?$/,
        exclude: /(node_modules|bower_components)/,
        loader: 'ts-loader',
      },
      {
        test: /\.less$/,
        exclude: /(node_modules|bower_components|js\/templates)/,
        loader: "style-loader!css-loader!less-loader"
      },
      {
        test: /\.css$/,
        // exclude: /(node_modules|bower_components|js\/templates)/,
        loader: "style-loader!css-loader"
      },
      {
        test: /\.woff(2)?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
        loader: "url-loader?limit=10000&mimetype=application/font-woff"
      },
      {
        test: /\.(svg|png)$/,
        include: [
          path.join(__dirname, "src/assets")
        ],
        exclude: /(node_modules|bower_components|typings)/,
        loader: "file-loader?name=./resources/[name].[ext]"
      },
      {
        test: /\.(ttf|eot|svg)(\?v=[0-9]\.[0-9]\.[0-9])?$/,
        exclude: /(resources)/,
        loader: "file-loader"
      }
    ]
  },
  resolve: {
    extensions: ['.ts', '.tsx', '.js', '.jsx', '.json'],
    modules: ['src', 'node_modules'],
  },
  output: {
    path: __dirname + "/dist/",
    filename: "client.min.js"
  },
  plugins: debug ? [
    new webpack.DefinePlugin({
      __DEBUG__: JSON.stringify(true),
      __CODE_VERSION__: JSON.stringify(codeVersion)
    }),
  ] : [
    new webpack.DefinePlugin({
      __DEBUG__: JSON.stringify(false),
      __CODE_VERSION__: JSON.stringify(codeVersion)
    }),
    new webpack.optimize.UglifyJsPlugin({
      compress: {
        warnings: false
      },
      mangle: true,
      sourcemap: false,
      output: {comments: false}
    })
  ],
};
