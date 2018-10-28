package com.tfwcn.blog.dao;

import com.tfwcn.blog.models.ErrorInfo;
import org.springframework.data.repository.CrudRepository;

import javax.transaction.Transactional;

/**
 * 错误操作接口
 */
@Transactional
public interface ErrorDao extends CrudRepository<ErrorInfo, String> {
    public ErrorInfo findByNum(long num);
}
