package com.tfwcn.blog.dao;

import com.tfwcn.blog.models.Notes;
import java.util.List;
import org.apache.ibatis.annotations.Mapper;

@Mapper
public interface NotesMapper {
    int deleteByPrimaryKey(String id);

    int insert(Notes record);

    Notes selectByPrimaryKey(String id);

    List<Notes> selectAll();

    int updateByPrimaryKey(Notes record);
}