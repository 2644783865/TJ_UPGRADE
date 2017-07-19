<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="CM_Tech_assign.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_Tech_assign" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register src="../Controls/UserDefinedQueryConditions.ascx" tagname="UserDefinedQueryConditions" tagprefix="uc3" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../Controls/JSRegister.ascx" tagname="JSRegister" tagprefix="JSR" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    技术任务表&nbsp;&nbsp;&nbsp;<span style="color:Red; font-size:smaller;">双击某带“-”任务号所在行，查看该任务号下所有数据</span>
    <link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" /> 
    <script type="text/javascript" language="javascript">
      function GetContent(obj)
      {
          if(obj.value.indexOf("‖")>0)
          {
             var querytype=document.getElementById("<%=ddlSearch.ClientID %>").value;
             var array_content=new Array();
             array_content=obj.value.split("‖");
             switch(querytype)
             {
                case "TSA_PJNAME":
                     obj.value=array_content[0];
                break;
                case "TSA_PJID":
                     obj.value=array_content[1];
                break;
                case "TSA_ENGNAME":
                     obj.value=array_content[2];
                break;
                case "TSA_ID":
                    obj.value=array_content[3];
                break;
                case "TSA_TCCLERKNM":
                    obj.value=array_content[4];
                break;
                default:
                break;
                
             }
          }
      }
      
  function ShowOrg(taskid)
  {
     var date=new Date();
     var time=date.getTime();
     window.open("TM_Task_View.aspx?NoUse="+time+"&action="+taskid);
  }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
<JSR:JSRegister ID="JSRegister1" runat="server" />
<script src="../JS/BOM.js" type="text/javascript" charset="GB2312"></script>
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>

    <div  class="box-inner">
        <div class="box_right">
            <div class="box-title">
            
                <table style="width:100%; height:24px">
                    <tr>
                    <td style="width:10%; height:24px"><b>技术任务分工</b></td>
                    <td align="right">按类查询:<asp:DropDownList ID="ddlSearch"  runat="server" 
                            onselectedindexchanged="ddlSearch_SelectedIndexChanged">
                            <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
                            <asp:ListItem Text="项目名称" Value="TSA_PJNAME"></asp:ListItem>
                            <asp:ListItem Text="项目代号" Value="TSA_PJID"></asp:ListItem>
                            <asp:ListItem Text="工程名称" Value="TSA_ENGNAME"></asp:ListItem>
                            <asp:ListItem Text="生产制号" Value="TSA_ID"></asp:ListItem>
                            <asp:ListItem Text="技术员" Value="TSA_TCCLERKNM"></asp:ListItem>
                        </asp:DropDownList></td>
                      <td align="left"> 
                        <asp:TextBox ID="txtSearch" runat="server" onfocus="this.select();" onchange="GetContent(this);" Width="120px"></asp:TextBox>
                          <cc1:AutoCompleteExtender ID="AutoCompleteExtender"  runat="server" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"  CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                DelimiterCharacters="" Enabled="True"  MinimumPrefixLength="1" ServiceMethod="GetSuggestions_Tech" FirstRowSelected="true" 
                                ServicePath="~/TM_Data/TM_WebService.asmx" TargetControlID="txtSearch" UseContextKey="True" CompletionInterval="10">
                          </cc1:AutoCompleteExtender>
                        <asp:Button ID="btnSearch" runat="server" Text=" 查 询 " onclick="btnSearch_Click"/>
                    </td>
                    <td align="right">
                    电气制号:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDQ" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged">
                        <asp:ListItem Text="--"  Selected="True"></asp:ListItem>              
                        <asp:ListItem Text="Y" value="Y"></asp:ListItem>
                        <asp:ListItem Text="N" Value="N" ></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right">排序:</td>
                    <td>
                        <asp:DropDownList ID="ddlSort" runat="server" AutoPostBack="true">
                          <asp:ListItem Text="生产制号" Value="[dbo].[TM_TaskOrder](TSA_ID)" Selected="True"></asp:ListItem>
                          <asp:ListItem Text="登记日期" Value="[dbo].[TM_TaskOrder2](TSA_ID),TSA_ADDTIME"></asp:ListItem>
                          <asp:ListItem Text="接收日期" Value="[dbo].[TM_TaskOrder2](TSA_ID),TSA_RECVDATE"></asp:ListItem>
                          <asp:ListItem Text="技术员" Value="[dbo].[TM_TaskOrder2](TSA_ID),TSA_TCCLERKNM"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                    <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server"><asp:Image ID="Image4" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2" 
                    align="absmiddle" runat="server" />其它筛选</asp:HyperLink>
                    <cc1:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false" Position="Left"  Enabled="true" runat="server" OffsetX="-400"  OffsetY="-25"  TargetControlID="HyperLink1" PopupControlID="palORG">
                    </cc1:PopupControlExtender>
                   <asp:Panel ID="palORG" Width="50%" style="display:none;visibility:hidden;border-style:solid;border-width:1px;border-color:blue;background-color:Menu;" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>  
                     <table width="100%" >
                     <tr>       
                     <td>
                          <div style="font-family: Verdana, Helvetica, Arial, sans-serif;line-height: 17px;font-size: 11px;font-weight: bold;position: absolute;top:8px;right: 10px;">
                              <a onclick="document.body.click(); return false;" style="background-color: #6699CC; cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;" title="关闭">X</a>
                          </div>
                          <br /><br />
                     </td>
                     </tr>
                        <tr>
                        <td align="right"><asp:Button ID="Button1" runat="server" UseSubmitBehavior="false" OnClick="btnSearch_Click" Text="查 询" />
                        &nbsp;&nbsp;<asp:Button ID="Button2" runat="server" UseSubmitBehavior="false" OnClick="btnMSClear_OnClick" Text="清 空" />&nbsp;&nbsp;</td>
                     </tr>
                      <tr>
                      <td align="left" style="width:98%">
                         <uc3:UserDefinedQueryConditions runat="server" ID="udqMS" QueryColumnsType="TaskidView"  QueryRows="4" />
                      </td>
                      </tr>
                     </table>
                     </ContentTemplate>
                     </asp:UpdatePanel>
                     </asp:Panel>
                    </td>
                    <td align="right">
                        <asp:HyperLink ID="HyperLink2" CssClass="link" runat="server" NavigateUrl="~/TM_Data/TM_Tech_Assign_OldAdd.aspx">
                         <asp:Image ID="Image1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                            旧制号录入
                        </asp:HyperLink>
                    </td>
                    <td align="right">
                        <asp:HyperLink ID="hlAdd" CssClass="link" runat="server" NavigateUrl="~/CM_Data/CM_Add_Assign.aspx?Action=Add">
                         <asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                            新增制号
                        </asp:HyperLink>
                    </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width:99%; overflow:auto;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server" OnRowDataBound ="GridView1_OnRowDataBound"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
            <FooterStyle BackColor="#507CD1" Font-Bold="True"  ForeColor="#1E5C95" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
            
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("TSA_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                
                <asp:BoundField DataField="TSA_ID" HeaderText="生产制号/任务号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="TSA_PJNAME" HeaderText="项目名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="TSA_ENGNAME" HeaderText="工程名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="TSA_NUMBER" HeaderText="台数" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="TSA_RECVDATE" HeaderText="接收日期" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                
                <asp:BoundField DataField="TSA_ADDTIME" HeaderText="登记日期" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="状态" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="tsa_status" runat="server" Text='<%# Eval("TSA_STATE").ToString()=="0"?"待分工":Eval("TSA_STATE").ToString()=="1"?"进行中...":Eval("TSA_STATE").ToString()=="2"?"完工":"停工" %>'></asp:Label>                       
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderText="删除" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                   <ItemTemplate>
                   <asp:Image ID="Image11" ImageUrl="~/Assets/images/res.gif" runat="server" border="0" hspace="2" align="absmiddle" />
                       <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_OnClick" runat="server" CommandArgument='<%# Eval("TSA_ID")%>' CommandName="Del" OnClientClick="return confirm('确认删除吗?')" >删除</asp:LinkButton>
                   </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle  CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />                    
        </asp:GridView>
                <asp:Panel ID="NoDataPanel" runat="server">没有记录!</asp:Panel>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
