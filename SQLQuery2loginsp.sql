create proc login_sp
(
@tc varchar(50),
@pwd varchar(50)
)
as
begin
 select count(*) from users where @tc=tc and @pwd=pwd
end