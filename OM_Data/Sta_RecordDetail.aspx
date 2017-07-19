<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="Sta_RecordDetail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.Sta_RecordDetail" Title="人员信息记录查看" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
人员信息记录查看
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
 <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
        ActiveTabIndex="0">
        <asp:TabPanel ID="Tab" runat="server" TabIndex="0" HeaderText="人员基本信息">
            <HeaderTemplate>
                人员基本信息
            </HeaderTemplate>
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server">
                    <div class="box-wrapper" style="text-align: center;">
                        <div class="box-outer">
                            <table cellpadding="4" cellspacing="1" style="background: #EEF7FD; white-space: nowrap;
                                text-align: left;" class="grid toptable" border="1">
                                <!--==============人员基本信息============-->
                                <tr>
                                    <td rowspan="10" style="text-align: center; width: 60px">
                                        人<br />
                                        员<br />
                                        基<br />
                                        本<br />
                                        信<br />
                                        息
                                    </td>
                                    <td style="width: 80px;">
                                        姓名：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_NAME" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width: 80px">
                                        性别：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ST_GENDER" runat="server" Width="80px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        民族：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ST_PEOPLE" runat="server" Width="100px">
                                        </asp:DropDownList>
                                    </td>
                                    <td rowspan="7" style="width: 150px">
                                        <asp:Image ID="showImage" runat="server" Width="100px" Height="120px" />
                                        <br />
                                        <asp:FileUpload ID="FileUploadupdate" runat="server" Width="150px" />
                                        <asp:Label ID="lblupload" runat="server"></asp:Label><asp:Label ID="JPG" runat="server"></asp:Label>
                                        <br />
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 80px">
                                        年龄：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_AGE" runat="server" onkeypress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false;"
                                            onblur="Check(this)"></asp:TextBox>
                                    </td>
                                    <td>
                                        出生日期：
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="ST_BIRTHDAY" class="easyui-datebox" editable="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        出生地
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_PROVINCE" runat="server" Width="50px"></asp:TextBox>省<asp:TextBox
                                            ID="ST_CITY" runat="server" Width="50px"></asp:TextBox>市
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        政治面貌：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ST_POLITICAL" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        加入时间：
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="ST_JRTIME" class="easyui-datebox" editable="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        籍贯
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_PROVINCEJ" runat="server" Width="50px"></asp:TextBox>省<asp:TextBox
                                            ID="ST_CITYJ" runat="server" Width="50px"></asp:TextBox>市
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        户口类型：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_REGIST" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        邮编：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_ZIP" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        档案所在：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_DASZ" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        身份证号：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_IDCARD" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        联系电话：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_TELE" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        紧急情况电话：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_TELEJJ" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        婚姻状况：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ST_MARRY" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        健康状况：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_HEALTH" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        参加工作时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_JOBTIME" runat="server" class="easyui-datebox" editable="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        所在部门：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DEP_NAME" runat="server" Width="120px" 
                                          >
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        岗位名称：
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="DEP_POSITION" runat="server" Width="120px">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="DEP_NAME" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        序列：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ST_SEQUEN" runat="server" Width="80px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        新工号：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_WORKNO" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        工号：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_WORKNOOLD" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        考勤机号：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_ATTANDNO" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        家庭电话：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_HOMETELE" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        户籍地址：
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="ST_ADDRESS" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        二级机构：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_DEPID1" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        联系地址：
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="ST_CONTACTADDRESS" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <!--===============教育经历=================-->
                                <tr>
                                    <td style="text-align: center; width: 60px" rowspan="5">
                                        教<br />
                                        育<br />
                                        经<br />
                                        历
                                    </td>
                                    <td>
                                        第一学历：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ST_XUELI" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        第一学历类型：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ST_XUELITYPE" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        学位：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ST_XUEWEI" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        毕业院校：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_BIYE" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        专业：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_ZHUANYE" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        毕业时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_BIYETIME" runat="server" class="easyui-datebox" editable="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        最高学历：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ST_XUELIHI" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        最高学历类型：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ST_XUELITYPEHI" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        最高学位：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ST_XUEWEIHI" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        最高院校：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_BIYEHI" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        最高专业：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_ZHUANYEHI" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        毕业时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_BIYETIMEHI" runat="server" class="easyui-datebox" editable="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        外语语种：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_WY" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        外语水平：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_YZ" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        备注（获得证书）：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_NOTE" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <!--===============职位信息=================-->
                                <tr>
                                    <td rowspan="3" style="text-align: center; width: 60px">
                                        职<br />
                                        称<br />
                                        技<br />
                                        能<br />
                                        信<br />
                                        息
                                    </td>
                                    <td>
                                        技术职务（称）：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ST_ZHICH" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        认定时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_EMPTIME" runat="server" class="easyui-datebox" editable="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        认定单位：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_PDDW" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        职能等级：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ST_ZHICHXU" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        认定时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_EMPTIMEXU" runat="server" class="easyui-datebox" editable="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        认定单位：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_PDDWXU" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        取得资格时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_GETTIME" runat="server" class="easyui-datebox" editable="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        聘任时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_INOUTNO" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        备注（获得证书）：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_LYTIME" runat="server" class="easyui-datebox" editable="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <!--===============合同信息=================-->
                                <tr>
                                    <td rowspan="4" style="text-align: center; width: 60px">
                                        合<br />
                                        同<br />
                                        信<br />
                                        息
                                    </td>
                                    <td>
                                        入职时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_INTIME" runat="server" class="easyui-datebox" editable="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        转正时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_ZHENG" runat="server" class="easyui-datebox" editable="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        合同主体：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_CONTR" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        合同期限：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_CONTRTIME" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        合同起始时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_CONTRSTART" runat="server" class="easyui-datebox" ></asp:TextBox>
                                    </td>
                                    <td>
                                        合同终止时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_CONTREND" runat="server" class="easyui-datebox" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        退休时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_RETIRE" runat="server" class="easyui-datebox" editable="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        办理用工时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_YGTIME" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        住房公积金缴纳时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_ZFTIME" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        社会保险缴纳时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_SBTIME" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        保密协议：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_SECRET" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        保险情况：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_BXQK" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <!--===============工资信息=================-->
                                <tbody  id="Salary">
                                    <tr>
                                        <td rowspan="3" style="text-align: center; width: 60px">
                                            薪<br />
                                            酬<br />
                                            信<br />
                                            息
                                        </td>
                                        <td>
                                            试用期工资：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ST_SYMONEY" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            试用期长：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ST_SYTIME" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            转正工资：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ST_ZZMONEY" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            调薪额度：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ST_TXED" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            调薪后额度：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ST_NEXTED" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            调整原因：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ST_REASON" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            岗位系数
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ST_GANGWEIXISHU" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </tbody>
                                <!--===============其他=================-->
                                <tr>
                                    <td rowspan="2" style="text-align: center; width: 60px">
                                        其<br />
                                        他
                                    </td>
                                    <td>
                                        工作证：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_JOBCARD" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        餐卡：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_RESTCARD" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        工作服：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_CLOTH" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        住宿：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_ZHUSU" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        手续：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="ST_SHOUXU" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <!--===============角色==================-->
                                <tr style="text-align: center; width: 60px">
                                    <td>
                                        角<br />
                                        色<br />
                                        添<br />
                                        加
                                    </td>
                                    <td colspan="6">
                                        <asp:CheckBoxList runat="server" ID="chk_Role" RepeatColumns="7" RepeatDirection="Horizontal"
                                            CellPadding="6" Style="text-align: left">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="Tab1" runat="server" TabIndex="1" HeaderText="工作履历和教育信息">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div style="width: 900px">
                            工作履历表：&nbsp;&nbsp;&nbsp;&nbsp;
                          
                            <table id="gr" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <asp:Repeater ID="Det_Repeater" runat="server">
                                    <HeaderTemplate>
                                        <tr align="center" class="tableTitle headcolor">
                                            <td width="50px">
                                                <strong>序号</strong>
                                            </td>
                                            <td>
                                                <strong>起&nbsp;/&nbsp;止年月</strong>
                                            </td>
                                            <td>
                                                <strong>工&nbsp;作&nbsp;单&nbsp;位</strong>
                                            </td>
                                            <td>
                                                <strong>职位或者岗位</strong>
                                            </td>
                                            <td>
                                                <strong>离职原因</strong>
                                            </td>
                                            <td>
                                                <strong>月薪</strong>
                                            </td>
                                            <td>
                                                <strong>证明人</strong>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                            <td>
                                                <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                                <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                                </asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ST_GZSTART" runat="server" Text='<%# Eval("ST_GZSTART")%>' Width="150px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ST_GZDW" runat="server" Text='<%# Eval("ST_GZDW")%>' Width="200px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ST_POSITION" runat="server" Text='<%# Eval("ST_POSITION")%>' Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ST_REAOUT" runat="server" Text='<%# Eval("ST_REAOUT")%>' Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ST_SALARY" runat="server" Text='<%# Eval("ST_SALARY")%>' Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ST_INDENTITY" runat="server" Text='<%# Eval("ST_INDENTITY")%>' Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                没有记录!</asp:Panel>
                            <br />
                            <div>
                               </div>
                        </div>
                        <br />
                        <br />
                        <div style="width: 900px">
                            教育及其培训记录经历：&nbsp;&nbsp;&nbsp;&nbsp;
                          
                            <table id="Table1" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <asp:Repeater ID="Det_Repeater1" runat="server">
                                    <HeaderTemplate>
                                        <tr align="center" class="tableTitle headcolor">
                                            <td width="50px">
                                                <strong>序号</strong>
                                            </td>
                                            <td>
                                                <strong>起&nbsp;/&nbsp;止年月</strong>
                                            </td>
                                            <td>
                                                <strong>学校名称及培训机构</strong>
                                            </td>
                                            <td>
                                                <strong>专业</strong>
                                            </td>
                                            <td>
                                                <strong>外语语种及程度</strong>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                            <td>
                                                <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                                <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                                </asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ST_JYTIME" runat="server" Text='<%# Eval("ST_JYTIME")%>' Width="150px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ST_SCHOOL" runat="server" Text='<%# Eval("ST_SCHOOL")%>' Width="200px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ST_ZHUAN" runat="server" Text='<%# Eval("ST_ZHUAN")%>' Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ST_ENGLISH" runat="server" Text='<%# Eval("ST_ENGLISH")%>' Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <asp:Panel ID="NoDataPanel1" runat="server" HorizontalAlign="Center">
                                没有记录!</asp:Panel>
                            <br />
                           
                        </div>
                        <br />
                        <br />
                        <div style="width: 900px">
                            家庭成员：&nbsp;&nbsp;&nbsp;&nbsp;
                           
                            <table id="Table2" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                                border="1">
                                <asp:Repeater ID="Det_Repeater2" runat="server">
                                    <HeaderTemplate>
                                        <tr align="center" class="tableTitle headcolor">
                                            <td width="50px">
                                                <strong>序号</strong>
                                            </td>
                                            <td>
                                                <strong>姓&nbsp;名</strong>
                                            </td>
                                            <td>
                                                <strong>年龄</strong>
                                            </td>
                                            <td>
                                                <strong>与本人关系</strong>
                                            </td>
                                            <td>
                                                <strong>工作单位及职务</strong>
                                            </td>
                                            <td>
                                                <strong>联系电话</strong>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                            <td>
                                                <%# Convert.ToInt32(Container.ItemIndex +1) %>
                                                <asp:CheckBox ID="chk" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                                </asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ST_NAME" runat="server" Text='<%# Eval("ST_NAME")%>' Width="80px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ST_AGE" runat="server" Text='<%# Eval("ST_AGE")%>' Width="80px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ST_RELATION" runat="server" Text='<%# Eval("ST_RELATION")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ST_WORK" runat="server" Text='<%# Eval("ST_WORK")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ST_TELE" runat="server" Text='<%# Eval("ST_TELE")%>' Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <asp:Panel ID="NoDataPanel2" runat="server" HorizontalAlign="Center">
                                没有记录!</asp:Panel>
                            <br />
                          
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
