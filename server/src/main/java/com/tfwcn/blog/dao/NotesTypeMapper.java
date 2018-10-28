package com.tfwcn.blog.dao;

import com.tfwcn.blog.models.NotesType;
import org.apache.ibatis.annotations.Mapper;

import java.util.List;

@Mapper
public interface NotesTypeMapper {
    int deleteByPrimaryKey(String id);

    int insert(NotesType record);

    NotesType selectByPrimaryKey(String id);

    List<NotesType> selectAll();

    int updateByPrimaryKey(NotesType record);
}