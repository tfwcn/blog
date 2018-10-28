package com.tfwcn.blog.dao;

import com.tfwcn.blog.models.RepliesScore;
import org.apache.ibatis.annotations.Mapper;

import java.util.List;

@Mapper
public interface RepliesScoreMapper {
    int deleteByPrimaryKey(String id);

    int insert(RepliesScore record);

    RepliesScore selectByPrimaryKey(String id);

    List<RepliesScore> selectAll();

    int updateByPrimaryKey(RepliesScore record);
}