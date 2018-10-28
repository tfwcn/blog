export default function({ $axios }) {

  // $axios.onRequest(res => {
  //   return res;
  // });

  // $axios.onRequestError(res => {
  //   return res;
  // });

  $axios.onResponse(res => {

    if (!('code' in res.data) == null || !('result' in res.data)) {
      throw new Error('Not standard response format');
    }

    res.data.result.src = res;

    return res.data.result;
  });

  // $axios.onResponseError(res => {
  //   return res;
  // });

  return $axios;
}