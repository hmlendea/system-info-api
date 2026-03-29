[![Donate](https://img.shields.io/badge/-%E2%99%A5%20Donate-%23ff69b4)](https://hmlendea.go.ro/fund.html) [![Latest GitHub release](https://img.shields.io/github/v/release/hmlendea/system-info-api)](https://github.com/hmlendea/system-info-api/releases/latest) [![Build Status](https://github.com/hmlendea/system-info-api/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hmlendea/system-info-api/actions/workflows/dotnet.yml)

# System Info API

## About

Simple ASP.NET Core REST API that exposes basic information about the host machine.

The service currently returns three categories of data:

- system information
- network information
- regional information

This project is intended for lightweight infrastructure tooling, status dashboards, system inventory jobs, and internal automation where a small authenticated HTTP endpoint is easier to integrate than shelling into a host.

## Features

- Returns operating system name, kernel version, architecture, hostname, and uptime
- Returns network information, including public IP address and hostname
- Returns regional information, including current system time and local time zone
- Supports Linux distro detection
- Supports ARM and ARM64 architecture reporting in addition to x86 and x64
- Signs responses using HMAC
- Protects requests with API-key authorization and replay-protection middleware
- Uses structured logging through `NuciLog`

## Technology Stack

- .NET 10.0
- ASP.NET Core Web API
- `NuciAPI`
- `NuciAPI.Controllers`
- `NuciAPI.Middleware`
- `NuciLog`
- `NuciSecurity.HMAC`
- `NuciWeb.HTTP`

## Project Structure

- `Api/Controllers` contains HTTP controllers
- `Api/Mapping` contains mappings from domain models to response DTOs
- `Api/Requests` contains API request models
- `Api/Responses` contains response envelopes and response objects
- `Configuration` contains configuration binding models
- `Logging` contains operation and log key definitions
- `Service` contains business logic and domain models

## Endpoint

The API currently exposes a single endpoint:

- `GET /SystemInfo`

The route comes from the controller name, so the default path is exactly `/SystemInfo`.

## Returned Data

The response contains three top-level objects.

### System

`system` contains host operating-system data:

- `os`: operating system name
- `kernel`: kernel version or kernel-like OS descriptor
- `arch`: architecture string such as `x86`, `x64`, `arm`, or `arm64`
- `hostname`: local machine name
- `uptime`: host uptime in seconds

Platform-specific behavior:

- On Linux, `os` is read from `PRETTY_NAME` in `/etc/os-release` when available
- On Linux, `kernel` is read from `/proc/sys/kernel/osrelease`
- On other platforms, the implementation falls back to `RuntimeInformation.OSDescription`

### Network

`network` currently contains:

- `public_ip`: public IP address as resolved by `NuciWeb.HTTP`
- `hostname`: local machine name

Note: `public_ip` depends on outbound network connectivity

### Region

`region` contains:

- `system_time`: current local system time in ISO 8601 format
- `time_zone`: local time zone string from `TimeZoneInfo.Local`

Notes:

- `system_time` is generated with the `o` format specifier, so it is an ISO 8601 round-trip timestamp
- `time_zone` is platform-dependent because .NET time zone identifiers differ between Linux and Windows

## Example Response Shape

The exact outer envelope is provided by the `NuciAPI` base response type, but the payload produced by this project has the following structure:

```json
{
	"system": {
		"os": "Arch Linux",
		"kernel": "6.19.6-arch1-1",
		"arch": "x64",
		"hostname": "my-host",
		"uptime": 123456
	},
	"network": {
		"public_ip": "203.0.113.10",
		"hostname": "my-host"
	},
	"region": {
		"system_time": "2026-03-16T13:47:09.3182334+02:00",
		"time_zone": "(UTC+02:00) Eastern European Time (Bucharest)"
	}
}
```

Note: The upstream `NuciAPI` response envelope adds metadata fields, and those will appear around this payload.

## Security

The application is configured with the following security-related middleware and behavior:

- request header validation
- replay protection
- API-key authorization
- HMAC response signing

The endpoint uses the configured API key through `NuciApiAuthorisation.ApiKey(...)`, and the response is signed using the configured HMAC signing key.

This means the service is intended for trusted internal clients that know:

- the correct API key
- the expected request format required by the `NuciAPI` middleware stack
- how to validate the response signature if needed

## Configuration

Configuration is bound from `appsettings.json` into `SecuritySettings` and `NuciLoggerSettings`.

### appsettings.json

```json
{
	"securitySettings": {
		"apiKey": "[[SYSTEM_INFO_API_KEY]]",
		"hmacSigningKey": "[[SYSTEM_INFO_HMAC_SIGNING_KEY]]"
	},
    "nuciLoggerSettings": {
        "logFilePath": "logfile.log",
        "isFileOutputEnabled": true
    }
}
```

### Required Settings

- `securitySettings.apiKey`: API key required by the endpoint authorization layer
- `securitySettings.hmacSigningKey`: secret used to sign responses

### Environment Variables

Because the application uses the default ASP.NET Core host builder, these values can also be supplied through environment variables:

```bash
SecuritySettings__ApiKey=your-api-key
SecuritySettings__HmacSigningKey=your-hmac-key
```

Using environment variables or a secrets manager is preferable to storing real secrets directly in `appsettings.json`.

## Running Locally

### Prerequisites

- .NET 10.0 SDK

### Restore Dependencies

```bash
dotnet restore
```

### Run the API

```bash
dotnet run
```

By default, ASP.NET Core will start the application using the URLs configured by the runtime environment.

### Build the Project

```bash
dotnet build
```

### Publish the Project

The repository includes a helper script:

```bash
./release.sh 1.2.3
```

The script downloads and executes an external deployment script for .NET 10.0 releases.

Before using it in production, review the referenced script and make sure that executing a remote script matches your deployment policy.

## Implementation Notes

### Operating System Detection

The service tries to return a user-friendly operating-system name instead of the low-level platform/version tuple.

- Linux: reads `PRETTY_NAME` from `/etc/os-release`
- other platforms: falls back to `RuntimeInformation.OSDescription`

### Kernel Detection

- Linux: reads `/proc/sys/kernel/osrelease`
- other platforms: currently falls back to `RuntimeInformation.OSDescription`

### Architecture Detection

Architecture is derived from `RuntimeInformation.OSArchitecture` and normalized to lower-case API values:

- `x86`
- `x64`
- `arm`
- `arm64`

### Uptime Calculation

Uptime is calculated from `Environment.TickCount64` and returned as whole seconds.

This is simple and portable, but it reflects process-environment tick timing rather than a platform-specific uptime command output.

## Limitations

- only one HTTP endpoint is currently exposed
- the network section does not currently expose local IP addresses
- public IP discovery depends on external network reachability
- time zone identifiers are platform-dependent
- the exact authentication header contract is defined by the `NuciAPI` libraries, not redefined in this repository

## Development Notes

Important service registrations:

- `ISystemInfoService` -> `SystemInfoService`
- `ILogger` -> `NuciLogger`

Important middleware enabled in startup:

- exception handling middleware from `NuciAPI`
- HTTPS redirection
- static file support
- header validation
- replay protection
- authorization

## Target Framework

The current package targets `.NET 10.0`.

## License

This project is licensed under the `GNU General Public License v3.0` or later. See [LICENSE](./LICENSE) for details.
