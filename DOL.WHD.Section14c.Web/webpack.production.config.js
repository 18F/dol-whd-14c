var webpack = require("webpack");

module.exports = {
    devtool: 'source-map',
	entry: './src/modules/app.js',
	output: {
		path: './dist/',
		filename: 'index.js'
	},
	//externals: {
    //},
    eslint: {
        failOnWarning: false,
        failOnError: true
    },
	devServer: {
		inline: true,
		port: 3333
	},
	module: {
        preLoaders: [
            {
                test: /\.jsx?$/,
                exclude: /node_modules/,
                loader: 'eslint'
            }
        ],
		loaders: [
			{
				test: /\.js$/,
				exclude: /node_modules/,
				loaders: ['ng-annotate','babel?presets[]=es2015']
			},
            {
                test: /\.html$/,
                exclude: /node_modules/,
                loader: 'html'
            },
            {
				test: /\.scss$/,
				exclude: /node_modules/,
				loaders: ['style', 'css', 'resolve-url', 'sass?sourceMap']
			},
			{ test: /\.css$/, loader: "style-loader!css-loader" },
  		    {
                test: /\.(png|gif|jpg|jpeg|svg)$/,
                loader: 'file-loader?name=images/[name].[ext]'
            },
            {
    			test: /\.(ttf|eot|woff|woff2)$/,
                loader: 'file-loader?name=fonts/[name].[ext]'
            }
		]
	},
	plugins: [
        new webpack.DefinePlugin({
            'process.env': {
                'NODE_ENV': JSON.stringify('production')
            }
        }),
        new webpack.optimize.UglifyJsPlugin({
            compressor: {
                warnings: false
            }
        })
	]
}
