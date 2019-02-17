package com.tfwcn.blog.models.api;

public class NotesListRequest {
    /**
     * 页数
     */
    private Integer page;

    /**
     * 每页行数
     */
    private Integer rows;

    public Integer getPage() {
        return page;
    }

    public void setPage(Integer page) {
        this.page = page;
    }

    public Integer getRows() {
        return rows;
    }

    public void setRows(Integer rows) {
        this.rows = rows;
    }
}
