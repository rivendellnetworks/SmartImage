# SmartImage

```
  ____                       _   ___
 / ___| _ __ ___   __ _ _ __| |_|_ _|_ __ ___   __ _  __ _  ___
 \___ \| '_ ` _ \ / _` | '__| __|| || '_ ` _ \ / _` |/ _` |/ _ \
  ___) | | | | | | (_| | |  | |_ | || | | | | | (_| | (_| |  __/
 |____/|_| |_| |_|\__,_|_|   \__|___|_| |_| |_|\__,_|\__, |\___|
                                                     |___/
```

*Find the source image in one click!*

SmartImage is a reverse image search tool for Windows with context menu integration. SmartImage will open the best match found returned from various image search engines (see the supported sites) right in your web browser. This behavior can be configured to the user's preferences.


### [Download](https://github.com/Decimation/SmartImage/releases)


# Supported sites

Supported sites:

- [SauceNao](https://saucenao.com/) (`SauceNao`)
- ImgOps (`ImgOps`)
- Google Images (`GoogleImages`)
- TinEye (`TinEye`)
- IQDB (`Iqdb`)
- trace.moe (`TraceMoe`)
- KarmaDecay (`KarmaDecay`)
- Yandex (`Yandex`)
- Bing (`Bing`)

Search engine names and configuration:

- `SauceNao`
- `ImgOps`
- `GoogleImages`
- `TinEye`
- `Iqdb`
- `TraceMoe`
- `KarmaDecay`
- `Yandex`
- `Bing`
- `All`
- `None`

# Download

**[See the latest releases](https://github.com/Decimation/SmartImage/releases)**

# Example

![Demo](https://github.com/Decimation/SmartImage/raw/master/Demo.gif)

![Context menu image](https://github.com/Decimation/SmartImage/blob/master/Context%20menu%20integration.png)

# Usage

SmartImage can be used in multiple ways:

- Open the program normally (double click) and you can use the program in a user-friendly way. You can then drag and drop your image into the command prompt and run a search.
- Right-click on an image (once the context menu integration is set up) and select the SmartImage option to immediately perform a search.
- Drag and drop an image over the executable to immediately perform a search (functionally the same as right-clicking on an image and using the SmartImage option).
- Use the command line which allows for more specific and advanced searching by using the listed arguments and options.

## Command Line

Command line syntax:

`smartimage <command> [options...]`

- Angle brackets (`<>`) specify required arguments.

- Square brackets (`[]`) specify optional arguments. 

- Ellipses (`...`) specify one or more arguments.

**Behavior note**: *Any options not specified via the command line are automatically read from the configuration file.*

### Options

`--engines <engines>`

Sets the search engines to use when searching, delimited by commas. See the above list for possible arguments. 
*Default: `All`*

`--priority-engines <engines>`

Sets the priority search engines, delimited by commands. See the above list for possible arguments. Priority search engines are engines whose results will be automatically opened in your browser when searching is complete. For example, if you designate `SauceNao` as a priority engine, then results returned by
`SauceNao` will be automatically opened in your browser. *Default: `SauceNao`*

`--saucenao-auth <api key>`

Configures the SauceNao API key. Register an application [here](https://saucenao.com/user.php), then get your key [here](https://saucenao.com/user.php?page=search-api).
If this is configured, SmartImage will use the SauceNao API instead of parsing the HTML response.

`--imgur-auth <consumer id>`

Configures Imgur API keys. Register an application [here](https://api.imgur.com/oauth2/addclient), then get your ID [here](https://imgur.com/account/settings/apps). If this is configured, SmartImage will use Imgur to upload temporary images instead of ImgOps.

`--update-cfg`

Updates the configuration file with the supplied command line arguments.


### Usage examples

`smartimage --engines All --priority-engines None "image.jpg"`

Runs the program using all search engines and no results will be in the browser.

`smartimage --engines SauceNao,ImgOps,GoogleImages --priority-engines SauceNao "image.jpg"`

Runs the program using SauceNao, ImgOps, and Google Images. The best result from SauceNao will be opened in the browser.

`smartimage --engines SauceNao,ImgOps,KarmaDecay --priority-engines SauceNao --update-cfg "image.jpg"`

Runs the program using SauceNao, ImgOps, and Karma Decay. The best result from SauceNao will be opened in the browser.
The specified options will be saved to the configuration file.


# Notes

- SmartImage may trigger an antivirus warning when first running. This is because SmartImage dynamically creates and runs batch files to add its context menu entry. Unfortunately this seems to be the only way to add context menu entries (for now).

- SmartImage must be added to the system PATH (*`%PATH%`*) environment variable, otherwise context menu integration will not work. SmartImage will automatically do this for you. Otherwise, you can read about how to manually do this [here](https://superuser.com/questions/949560/how-do-i-set-system-environment-variables-in-windows-10).

- SmartImage uploads temporary images using ImgOps (the uploaded images are automatically deleted after 2 hours). Imgur can also be used, but you must register an Imgur application client.

- Some functions use hacky solutions (like dynamically creating a registry key file to install context menu integration). This is temporary until I can find better approaches, but it should work in the meantime.

# to-do

- Add a better icon

- Further rewrite & refactor codebase

- Find better, less hacky approaches to various functions

- Update examples

# Inspiration

- [SauceNao-Windows](https://github.com/RoxasShadow/SauceNao-Windows)
- [SharpNao](https://github.com/Lazrius/SharpNao)
