package com.tfwcn.blog.dao;

import com.tfwcn.blog.models.NoteTypeInfo;
import org.springframework.data.repository.CrudRepository;

import javax.transaction.Transactional;

/**
 * 贴子类型 操作接口
 */
@Transactional
public interface NoteTypeDao extends CrudRepository<NoteTypeInfo, String> {
    public NoteTypeInfo findByNum(Long num);
}
