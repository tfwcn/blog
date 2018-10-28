package com.tfwcn.blog.models;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;
import java.util.Date;

/**
 * 错误信息 类
 */
@Entity
@Table(name = "error")
public class ErrorInfo {
    public ErrorInfo() {

    }

    /**
     * GUID
     */
    @Id
    @Column(name = "id", nullable = false, length = 32)
    private String id;

    /**
     * 流水号
     */
    @Column(name = "num", nullable = false, length = 20)
    private Long num;

    /**
     * 错误信息
     */
    @Column(name = "message", length = 200)
    private String message;

    /**
     * 错误明细
     */
    @Column(name = "detail")
    private String detail;

    /**
     * 创建时间
     */
    @Column(name = "create_time", nullable = false)
    private Date createTime;

    /**
     * 最后更新时间
     */
    @Column(name = "update_time")
    private Date updateTime;

    /**
     * 设置 GUID
     */
    public String getId() {
        return id;
    }

    /**
     * 获取 GUID
     */
    public void setId(String id) {
        this.id = id;
    }

    /**
     * 设置 流水号
     */
    public Long getNum() {
        return num;
    }

    /**
     * 获取 流水号
     */
    public void setNum(Long num) {
        this.num = num;
    }

    /**
     * 设置 错误信息
     */
    public String getMessage() {
        return message;
    }

    /**
     * 获取 错误信息
     */
    public void setMessage(String message) {
        this.message = message;
    }

    /**
     * 设置 错误明细
     */
    public String getDetail() {
        return detail;
    }

    /**
     * 获取 错误明细
     */
    public void setDetail(String detail) {
        if (detail.length() > 1000)
            detail = detail.substring(0, 1000);
        this.detail = detail;
    }

    /**
     * 设置 创建时间
     */
    public Date getCreateTime() {
        return createTime;
    }

    /**
     * 获取 创建时间
     */
    public void setCreateTime(Date createTime) {
        this.createTime = createTime;
    }

    /**
     * 设置 最后更新时间
     */
    public Date getUpdateTime() {
        return updateTime;
    }

    /**
     * 获取 最后更新时间
     */
    public void setUpdateTime(Date updateTime) {
        this.updateTime = updateTime;
    }
}
