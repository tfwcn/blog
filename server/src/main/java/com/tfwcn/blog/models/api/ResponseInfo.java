package com.tfwcn.blog.models.api;

/**
 * 请求返回类
 */
public class ResponseInfo<T> {
    public ResponseInfo() {
    }

    public ResponseInfo(int code, T result) {
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
    private T result;

    public int getCode() {
        return code;
    }

    public void setCode(int code) {
        this.code = code;
    }

    public T getResult() {
        return result;
    }

    public void setResult(T result) {
        this.result = result;
    }
}
