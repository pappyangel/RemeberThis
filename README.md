# RememberThis

### The following list includes the work done in the Read Branch

### The following list includes the work done in the MultiPart Branch

- Added a B2C project as a template
- Blazor Component calling an API
  - added HTTPClientFactory
  - pass both class data and file to API
- Updated to Bootstrap 5.2.3
- Added Bootstrap Modal dialog box
- Created Modal as child component
  - eventcallback parameter
  - added fancy icons
- Added Try Catch on call to API - PostAsync
  - This is for the use case where we have not yet started the API, but the client calls it
- Added File Validation logic
  - file size
  - extension type
  - header check
- Azure Storage
  - added code to write image to storage
    - using client secrets
  - generate safe file name
- SQL
  - created class for CRUD operations to SQL
  - copied from Cocktail project
  - using client secrets for pswd at this point
  - only insert statement completed

