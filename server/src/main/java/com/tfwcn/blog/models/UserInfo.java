package com.tfwcn.blog.models;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;
import java.util.Date;

/**
 * 用户信息 类
 */
@Entity
@Table(name = "users")
public class UserInfo {
    public UserInfo() {

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
     * 用户名
     */
    @Column(name = "user_name", nullable = false, length = 20)
    private String userName;

    /**
     * 登录账号
     */
    @Column(name = "login_name", nullable = false, length = 50)
    private String loginName;

    /**
     * 密码
     */
    @Column(name = "password", nullable = false, length = 32)
    private String password;

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
     * 设置 用户名
     */
    public String getUserName() {
        return userName;
    }

    /**
     * 获取 用户名
     */
    public void setUserName(String userName) {
        this.userName = userName;
    }

    /**
     * 设置 登录账号
     */
    public String getLoginName() {
        return loginName;
    }

    /**
     * 获取 登录账号
     */
    public void setLoginName(String loginName) {
        this.loginName = loginName;
    }

    /**
     * 设置 密码
     */
    public String getPassword() {
        return password;
    }

    /**
     * 获取 密码
     */
    public void setPassword(String password) {
        this.password = password;
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
