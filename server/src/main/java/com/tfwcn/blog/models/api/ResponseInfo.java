package com.tfwcn.blog.models.api;

/**
 * 请求返回类
 */
public class ResponseInfo {
    public ResponseInfo() {
    }

    public ResponseInfo(int code, Object result) {
        this.code = code;
        this.result = result;
    }
    /**
     * 代码，1：成功
     */
    private int code;

    /**
     * 返回值
     */
    private Object result;

    public int getCode() {
        return code;
    }

    public void setCode(int code) {
        this.code = code;
    }

    public Object getResult() {
        return result;
    }

    public void setResult(Object result) {
        this.result = result;
    }
}
