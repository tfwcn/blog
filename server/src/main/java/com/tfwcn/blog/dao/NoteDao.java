package com.tfwcn.blog.dao;

import com.tfwcn.blog.models.NoteInfo;
import org.springframework.data.repository.CrudRepository;

import javax.transaction.Transactional;

/**
 * 贴子信息 操作接口
 */
@Transactional
public interface NoteDao extends CrudRepository<NoteInfo, String> {
    public NoteInfo findByNum(Long num);
}
