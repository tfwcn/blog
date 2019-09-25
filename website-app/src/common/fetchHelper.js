export const postData = (url, data) => {
    // Default options are marked with *
    return fetch(url, {
        body: JSON.stringify(data), // must match 'Content-Type' header
        cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        credentials: 'same-origin', // include, same-origin, *omit
        headers: {
            'user-agent': 'Mozilla/4.0 MDN Example',
            'content-type': 'application/json',
            'Access-Token': sessionStorage.getItem('access_token') || ''
        },
        method: 'POST', // *GET, POST, PUT, DELETE, etc.
        mode: 'cors', // no-cors, cors, *same-origin
        redirect: 'follow', // manual, *follow, error
        referrer: 'no-referrer', // *client, no-referrer
    }).then(response => {
        console.log(response);
        if (response.status === 200)
            return response.json();
        else if (response.status === 401){
            throw new Error('未授权');
        }
        else
            throw new Error('请求异常' + response.status);
    }); // parses response to JSON
}