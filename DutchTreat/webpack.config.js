const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const UglifyJsPlugin = require("uglifyjs-webpack-plugin");
const OptimizeCSSAssetsPlugin = require("optimize-css-assets-webpack-plugin");

module.exports = (env = {}, argv = {}) => {
    const isProd = argv.mode === 'production';

    const config = {
        mode: argv.mode || 'development',
        entry: {
            main: './Resources/js/main.js',
            validation: './Resources/js/validation.js'
        },
        output: {
            filename: '[name]-packed.min.js',
            path: path.resolve(__dirname, './wwwroot/dist/'),
            publicPath: "/dist/"
        },
        plugins: [
            new MiniCssExtractPlugin({
                filename: 'styles.min.css'
            })
        ],
        module: {
            rules: [
                {
                    test: /\.css$/,
                    use: [
                        isProd ? MiniCssExtractPlugin.loader : 'style-loader',
                        {
                            loader: 'css-loader'
                        }
                    ]
                }
            ]
        },
        optimization: {
            minimizer: [
                new UglifyJsPlugin({
                    cache: true,
                    parallel: true,
                    sourceMap: true,
                    extractComments: true
                }),
                new OptimizeCSSAssetsPlugin({
                    cssProcessorPluginOptions: {
                        preset: ['default', { discardComments: { removeAll: true } }]
                    }
                })
            ]
        }
    }

    return config;
};