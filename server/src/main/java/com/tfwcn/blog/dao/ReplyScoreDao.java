package com.tfwcn.blog.dao;

import com.tfwcn.blog.models.ReplyScoreInfo;
import org.springframework.data.repository.CrudRepository;

import javax.transaction.Transactional;

/**
 * 回复评分记录 操作接口
 */
@Transactional
public interface ReplyScoreDao extends CrudRepository<ReplyScoreInfo, String> {
    public ReplyScoreInfo findByNum(Long num);
}
