var webpack = require('webpack');
var path = require('path');
var bourbon = require('node-bourbon').includePaths;

module.exports = {
    devtool: 'eval-source-map',
    entry: './src/modules/app.js',
    output: {
        path: './',
        filename: 'index.js'
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
            { test: /\.css$/, loader: 'style-loader!css-loader' },
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
