let serviceUrl = "http://localhost:1500/"

export const API_URLS = {
    CartList: {
        user: 'https://jsonplaceholder.typicode.com/users'
    },
    RoleMast: {
        Select: `${serviceUrl}/RoleMast/Select`,
    },
    User: {
        Select: `${serviceUrl}/UserMast/Select`,
    },
    PageList: {
        Select: `${serviceUrl}/PageList/Select`,
    },
    PageControls: {
        Select: `${serviceUrl}/PageControls/Select`,
        Insert: `${serviceUrl}/PageControls/Insert`,
        Delete: `${serviceUrl}/PageControls/Delete`
    }
}