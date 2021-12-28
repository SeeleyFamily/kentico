-- simple check for -(n) naming issues
select * from View_CMS_Tree_Joined where nodealias like '%-(_)%' or nodename like '%(_)%'