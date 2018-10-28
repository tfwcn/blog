package com.tfwcn.blog.dao;

import com.tfwcn.blog.models.Notes;
import org.apache.ibatis.annotations.Mapper;

import java.util.List;

@Mapper
public interface NotesMapper {
    int deleteByPrimaryKey(String id);

    int insert(Notes record);

    Notes selectByPrimaryKey(String id);

    List<Notes> selectAll();

    int updateByPrimaryKey(Notes record);
}