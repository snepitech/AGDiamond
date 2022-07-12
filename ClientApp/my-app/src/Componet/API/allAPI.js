let serviceUrl = "http://localhost:1500/"

export const API_URLS = {
    CartList: {
        user: 'https://jsonplaceholder.typicode.com/users'
    },
    RoleMast: {
        Select: `${serviceUrl}/RoleMast/Select`,
        Insert: `${serviceUrl}/RoleMast/Insert`,
        Update: `${serviceUrl}/RoleMast/Update`,
        Delete: `${serviceUrl}/RoleMast/Delete`,
    },
    User: {
        Select: `${serviceUrl}/UserMast/Select`,
    },
    PageList: {
        Select: `${serviceUrl}/PageList/Select`,
        Insert: `${serviceUrl}/PageList/Insert`,
        Update: `${serviceUrl}/PageList/Update`,
        Delete: `${serviceUrl}/PageList/Delete`,
    },
    PageControls: {
        Select: `${serviceUrl}/PageControls/Select`,
        Insert: `${serviceUrl}/PageControls/Insert`,
        Delete: `${serviceUrl}/PageControls/Delete`,
    }
}