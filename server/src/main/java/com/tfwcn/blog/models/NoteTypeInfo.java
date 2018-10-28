package com.tfwcn.blog.models;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;
import java.util.Date;

/**
 * 贴子类型 类
 */
@Entity
@Table(name = "note_type")
public class NoteTypeInfo {
    public NoteTypeInfo() {
        
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
     * 类型名称
     */
    @Column(name = "name", length = 20)
    private String name;

    /**
     * 类型描述
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
     * 设置 类型名称
     */
    public String getName() {
        return name;
    }

    /**
     * 获取 类型名称
     */
    public void setName(String name) {
        this.name = name;
    }

    /**
     * 设置 类型描述
     */
    public String getDetail() {
        return detail;
    }

    /**
     * 获取 类型描述
     */
    public void setDetail(String detail) {
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
