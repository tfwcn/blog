package com.tfwcn.blog.dao;

import com.tfwcn.blog.models.ReplyInfo;
import org.springframework.data.repository.CrudRepository;

import javax.transaction.Transactional;

/**
 * 回复信息 操作接口
 */
@Transactional
public interface ReplyDao extends CrudRepository<ReplyInfo, String> {
    public ReplyInfo findByNum(Long num);
}
