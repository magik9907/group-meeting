const path = require('path')
const webpack = require('webpack')
const fs = require('fs')
const MiniCssExtractPlugin = require('mini-css-extract-plugin')
const { CleanWebpackPlugin } = require('clean-webpack-plugin')

const ENV_SETTINGS = {
  /* Path to source files directory */
  source: path.resolve(__dirname, 'src'),
  /* Path to built files directory */
  output: path.resolve(__dirname, 'wwwroot/dist'),
  limits: {
    /* Image files size in bytes. Below this value the image file will be served as DataURL (inline base64). */
    images: 8192,
    /* Font files size in bytes. Below this value the font file will be served as DataURL (inline base64). */
    fonts: 8192,
  },
}

const configPath = `${ENV_SETTINGS.source}/utilities/`
const utilitiesFiles = fs.readdirSync(path.resolve(__dirname, configPath))
const resourcesConfigFiles = utilitiesFiles.map((file) =>
  path.resolve(__dirname, configPath, file)
)

module.exports = {
  entry: path.resolve(ENV_SETTINGS.source, 'app.js'),
  output: {
    path: ENV_SETTINGS.output,
    filename: 'js/[name].js',
  },
  module: {
    rules: [
      {
        test: /\.((c|sa|sc)ss)$/i,
        use: [
          MiniCssExtractPlugin.loader,
          'css-loader',
          'postcss-loader',
          'sass-loader',
          {
            loader: 'sass-resources-loader',
            options: {
              resources: resourcesConfigFiles,
            },
          },
        ],
      },
      {
        test: /\.js$/,
        exclude: /node_modules/,
        use: ['babel-loader'],
      },
      {
        test: /\.(png|gif|jpg|jpeg)$/,
        use: [
          {
            loader: 'url-loader',
            options: {
              name: 'assets/images/[name].[ext]',
              limit: ENV_SETTINGS.limits.images,
              esModule: false,
            },
          },
        ],
      },
      {
        test: /\.(eot|svg|otf|ttf|woff|woff2)$/,
        use: [
          {
            loader: 'url-loader',
            options: {
              name: 'assets/fonts/[name].[hash:6].[ext]',
              publicPath: '../',
              limit: ENV_SETTINGS.limits.fonts,
            },
          },
        ],
      },
    ],
  },
  devtool: 'source-map',
  devServer: {
    contentBase: ENV_SETTINGS.output,
    watchContentBase: true,
    publicPath: '/',
    open: true,
    historyApiFallback: {
      disableDotRule: true,
    },
    compress: true,
    overlay: true,
    hot: false,
    watchOptions: {
      poll: 300,
    },
    host: 'localhost',
    port: 8000,
  },
  watchOptions: {
    aggregateTimeout: 300,
    poll: 300,
    ignored: /node_modules/,
  },
  performance: { hints: false },
  plugins: [
    new MiniCssExtractPlugin({
      filename: 'css/[name].css',
    }),
  ],
}
