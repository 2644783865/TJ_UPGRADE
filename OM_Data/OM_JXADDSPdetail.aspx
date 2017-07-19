<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_JXADDSPdetail.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_JXADDSPdetail" Title="人员绩效审批" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
人员绩效审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <script src="../JS/PickPersons.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">
    var i;
        array = new Array();
        function SelPersons1() {
            $("#hidPerson").val("first");
            SelPersons();
        }
        
        function SelPersons2() {
            $("#hidPerson").val("second");
            SelPersons();
        }
    
     //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "first") {
                $("#<%=txt_first.ClientID %>").val(r.st_name);
                $("#<%=firstid.ClientID %>").val(r.st_id);
            }
            else if (id == "second") {
                $("#<%=txt_second.ClientID %>").val(r.st_name);
                $("#<%=secondid.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </asp:ToolkitScriptManager>
    <div style="width: 100%">
         <table width="100%">
               <tr>
                    <td align="right" align="right">
                        <asp:Button runat="server" ID="btnfanshen" Text="反审" OnClick="btnfanshen_OnClick" Visible="false" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnSave" Text="提交" OnClick="btnSave_OnClick" Visible="false" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
               </tr>
         </table>
    </div>
       <asp:TabContainer runat="server" ID="TabContainer1" TabStripPlacement="Top" 
        ActiveTabIndex="0" >
         <asp:TabPanel runat="server" ID="TabPanel1" HeaderText="数据明细" TabIndex="0">
            <ContentTemplate>
              <div style="width: 100%">
                  <table width="100%">
                    <tr>
                        <td>
                           发起人：
                           <asp:Label ID="lbfqrname" runat="server"></asp:Label>
                           <asp:Label ID="lbfqrid" runat="server" Visible="false"></asp:Label>
                           
                        </td>
                        <td>
                           发起时间：
                           <asp:Label ID="lbfqtime" runat="server"></asp:Label>
                        </td>
                    </tr>
                  </table>
              </div>
              <div style="width: 100%">
                  <table width="100%">
                    <tr>
                        <td align="left">
                            姓名：
                            <asp:TextBox ID="txtname" runat="server" OnTextChanged="Textname_TextChanged" Text='<%#Eval("gztz_name")%>'
                                   AutoPostBack="true" onclick="this.select();" Enabled="false"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="name_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                                DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="get_st_id"
                                ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="txtname"
                                UseContextKey="True">
                            </asp:AutoCompleteExtender>
                            <asp:Label ID="lbstid" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td align="left">
                            部门：<asp:TextBox ID="txtdepartment" runat="server" Width="100px" onfocus="this.blur()" ></asp:TextBox>
                        </td>
                        
                        <td align="left">
                            绩效工资系数：<asp:TextBox ID="txt_jxgzxs" runat="server" Width="100px" Enabled="false" ></asp:TextBox>
                        </td>
                        <td align="left">
                            开始年月：
                           <asp:TextBox ID="tb_yearmonth" Width="90px" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月"
                ID="CalendarExtender1" runat="server" Format="yyyy-MM" TargetControlID="tb_yearmonth">
                                        </asp:CalendarExtender>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td align="left">
                            类型：<asp:DropDownList ID="ddlType" runat="server" Width="100px" AutoPostBack="true">
                                            <asp:ListItem Text="--请选择--" Value="" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="绩效工资系数调整" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="人员绩效减少" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="新增人员绩效" Value="3"></asp:ListItem>
                                  </asp:DropDownList>
                        </td>
                    </tr>
                  </table>
              </div>
              <div>
                 <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                        border="1">
                        <tr>
                            <td align="center">
                                <asp:Label ID="title" runat="server" Text="内容"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:TextBox ID="txt_contents" runat="server" Width="80%" TextMode="MultiLine" Height="50px" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        
        <asp:TabPanel runat="server" ID="TabPanel2" TabIndex="2" Width="100%" HeaderText="审核">
            <ContentTemplate>
                <div class="box-wrapper">
                    <div style="height: 6px" class="box_top">
                    </div>
                    <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:RadioButtonList ID="rblSHJS" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rblSHJS_OnSelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="一级审核" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="二级审核" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="font-size: large; text-align: center; height: 43px">
                                    <asp:Label ID="lbtitle" runat="server"></asp:Label>
                                    <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                        Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="box-outer">
                        <table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                            border="1">
                            <tr id="yjshh" runat="server">
                                <td align="center">
                                    综合办公室
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_first" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect1" Visible="false" runat="server" CssClass="hand" onClick="SelPersons1()">
                                                    <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblfirst" Enabled="false" RepeatColumns="2" runat="server" Height="20px">
                                                    <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="first_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="opinion1" runat="server" Enabled="false" Height="42px" TextMode="MultiLine"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="ejshh" runat="server">
                                <td align="center">
                                    主管领导
                                </td>
                                <td colspan="3">
                                    <table align="center" width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr>
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_second" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect2" runat="server" Visible="false" CssClass="hand" onClick="SelPersons2()">
                                                    <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblsecond" RepeatColumns="2" runat="server" Enabled="false" Height="20px">
                                                    <asp:ListItem Text="同意" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="second_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="opinion2" runat="server" Enabled="false" TextMode="MultiLine" Width="100%"
                                                    Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>  
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
    
    <div>
        <div>
            <div id="win" visible="false">
                <div>
                    <table>
                        <tr>
                            <td>
                                <strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                按部门查询：
                            </td>
                            <td>
                                <input id="dep" name="dept"/>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 430px; height: 230px">
                    <table id="dg">
                    </table>
                </div>
            </div>
            <div id="buttons" style="text-align: right" visible="false">
                <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="return savePick();">
                    保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                        onclick="javascript:$('#win').dialog('close')">取消</a>
            </div>
        </div>
        <input type="hidden" id="hidPerson" value="" />
    </div>
</asp:Content>
