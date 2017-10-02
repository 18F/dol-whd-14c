var webpack = require('webpack');
var path = require('path');
var bourbon = require('node-bourbon').includePaths;
var helpers = require('./helpers');

module.exports = {
  devtool: 'eval-source-map',
  entry: './src/main.ts',
  output: {
    path: './',
    filename: 'index.js'
  },
  resolve: {
    extensions: ['', '.ts', '.js', '.json']
  },
  noInfo: true,
  quiet: true,
  module: {
    preLoaders: [
      {
        test: /\.jsx?$/,
        exclude: /node_modules/,
        loader: 'eslint'
      },
      {
        test: /\.js$/,
        include: path.resolve('src/modules/'),
        loader: 'istanbul-instrumenter',
        query: {
          esModules: true
        }
      }
    ],
    loaders: [
      {
        test: /\.ts$/,
        loaders: ['ts-loader', 'angular2-template-loader']
      },
      {
        test: /\.js$/,
        exclude: /node_modules/,
        loader: 'babel',
        query: {
          presets: ['es2015']
        }
      },
      {
        test: /\.html$/,
        exclude: /node_modules/,
        loader: 'html'
      },
      {
        test: /\.scss$/,
        exclude: /node_modules/,
        loaders: [
          'style',
          'css',
          'resolve-url',
          'sass?sourceMap&includePaths[]=' + bourbon
        ]
      },
      {
        test: /\.css$/,
        exclude: helpers.root('src', 'v4'),
        loader: 'style-loader!css-loader'
      },
      {
        test: /\.css$/,
        include: helpers.root('src', 'v4'),
        loader: 'raw-loader'
      },
      {
        test: /\.(png|gif|jpg|jpeg)$/,
        loader: 'url-loader?name=images/[name].[ext]'
      },
      {
        test: /\.(eot|svg|ttf|woff(2)?)(\?v=\d+\.\d+\.\d+)?/,
        loader: 'file-loader?name=fonts/[name].[ext]'
      }
    ]
  },
  plugins: [
    new webpack.ProvidePlugin({
      $: 'jquery',
      jQuery: 'jquery'
    })
  ]
};
