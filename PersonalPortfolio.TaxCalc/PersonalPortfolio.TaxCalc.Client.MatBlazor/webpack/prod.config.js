var path = require("path");
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const TerserPlugin  = require('terser-webpack-plugin');

const debugMode = false;

module.exports = {
    // ...
    node: {
      Buffer: false,
      process: false,
    },
  };

module.exports = {
  entry: {
    'matBlazor': [
      './src/main.js',
      './src/main.scss'
    ]
  },
  optimization: {
    minimize: !debugMode
  },
  output: {
    filename: "matBlazor.js",
    path: path.resolve(__dirname, '../../PersonalPortfolio.TaxCalc.Client/wwwroot'),
  },

  resolve: {
    extensions: [".ts", ".tsx", ".js", ".css", ".scss"]
  },

  module: {
    rules: [
      {
        test: /\.tsx?$/,
        use: 'ts-loader',
        exclude: /node_modules/,
      },
      {
        test: /\.js$/,
        use: {
          loader: "babel-loader",
          options: {

          }
        }
      },
      {
        test: /\.scss$/,
        use: [
          MiniCssExtractPlugin.loader,
          {
            loader: "css-loader" // translates CSS into CommonJS
          },
          {
            loader: "sass-loader", // compiles Sass to CSS
            options: {
              webpackImporter: false, // Recommended temporary workaround until https://github.com/webpack-contrib/sass-loader/issues/804 is fixed
              sassOptions: {
                "includePaths": [
                  path.resolve(__dirname, '../node_modules')
                ]
              },
            }
          }
        ]
      }
    ]
  },

  plugins: [
    new MiniCssExtractPlugin({
      filename: 'matBlazor.css'
    }),
    new TerserPlugin({
      parallel: true,
      terserOptions: {
        compress: {
          drop_debugger: !debugMode
        },
        mangle: debugMode,
        output: {
          comments: debugMode,
          beautify: debugMode
        }
      }
    }),
  ]
};
