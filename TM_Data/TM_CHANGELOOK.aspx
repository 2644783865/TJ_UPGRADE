<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_CHANGELOOK.aspx.cs"
    Inherits="ZCZJ_DPF.TM_Data.TM_CHANGELOOK" MasterPageFile="~/Masters/RightCotentMaster.master"
    Title="变更物料备库" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    变更物料备库
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <script language="javascript" type="text/javascript">
        function viewCondition()
        {
           document.getElementById("<%=PanelCondition.ClientID%>").style.display='block';
        }
        
        function SelectAll(obj)
        {
             var table=document.getElementById("<%=GridView1.ClientID%>");
             if(obj.checked)
             {
                 for(i=1;i<table.rows.length;i++)
                 {
                    objstr=table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                    if(objstr!=null)
                    {
                      objstr.checked=true;
                    }
                 }
             }
             else
             {
                for(i=1;i<table.rows.length;i++)
                {
                    objstr=table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                    if(objstr!=null)
                    {
                      objstr.checked=false;
                    }
                }
             }
        }   
        
        function ToStore()
        {
            var table=document.getElementById("<%=GridView1.ClientID%>");
            var count=0;
            var objstr;
            for(i=1;i<table.rows.length;i++)
            {
                objstr=table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                if(objstr!=null&&objstr.checked)
                {
                   count++;
                }
            }
            
            if(count==0)
            {
               alert("请勾选需要操作记录！！！");
               return false;
            }
            else
            {
               var cfm=confirm("共选择了【"+count+"】条记录，确认提交吗？");
               return cfm;
            }
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table style="border: 0px; border-style: ridge;" align="center" width="100%">
                            <tr>
                                <td align="right">
                                    <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                        PopupControlID="UpdatePanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                        Y="0">
                                    </asp:ModalPopupExtender>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="PanelCondition" runat="server" Width="95%" Style="display: none">
                            <asp:UpdatePanel ID="UpdatePanelCondition" runat="server">
                                <ContentTemplate>
                                    <table width="95%" style="background-color: #CCCCFF; border: solid 1px black;">
                                        <tr>
                                            <td colspan="8" align="center">
                                                <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="查询" />
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnClose" runat="server" Text="关闭" OnClick="btnClose_Click" />&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                计划跟踪号：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tb_ptcode" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                项目：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tb_pjnm" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                工程：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tb_engnm" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                物料编码：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tb_marid" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                名称：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tb_marnm" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                规格：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tb_margg" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                材质：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tb_marcz" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                国标：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tb_margb" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                图号：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTuhao" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                提交人：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSubID" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                执行人：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtExecID" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                备注：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBK_Note" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                      </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
                <div class="box-outer">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;全选/取消<input id="ckbSelectAll" onclick="SelectAll(this);"
                                    type="checkbox" />&nbsp;&nbsp;
                                <asp:Button ID="btnSelect" ToolTip="选择起止项后点击连选,起止项间所有记录选中" runat="server" OnClick="btnSelect_OnClick"
                                    Text="连选" />
                            </td>
                            <td align="left">
                                <asp:RadioButtonList ID="rblCheckType" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblCheckType_OnSelectedIndexChanged" runat="server">
                                  <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                  <asp:ListItem Text="未执行" Value="1"></asp:ListItem>
                                  <asp:ListItem Text="我的任务" Value="2" Selected="True"></asp:ListItem>
                                  <asp:ListItem Text="待备库" Value="3"></asp:ListItem>
                                  <asp:ListItem Text="调整中" Value="4"></asp:ListItem>
                                  <asp:ListItem Value="5" Text="已调整"></asp:ListItem>
                                 <asp:ListItem Value="6"  Text="关闭"></asp:ListItem>

                                </asp:RadioButtonList>
                            </td>
                            <td>
                               <asp:Label ID="LabelBKPerson" runat="server" Text="储运备库人:" Visible="false"></asp:Label> <asp:DropDownList ID="ddlBKPersonName" runat="server" Visible="false">
                                </asp:DropDownList>
                            </td>
                            <td>
                            <asp:Button ID="btnShowPopup" runat="server" Text="更多筛选..." OnClientClick="viewCondition()" />&nbsp;
                            </td>
                            <td align="center">
                                <asp:Button ID="btnCancleBK" runat="server" Text="取消备库" OnClientClick="return ToStore();" Visible="false" OnClick="btnCancleBK_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnToStore" runat="server" Text="通知储运备库" OnClientClick="return ToStore();" Visible="false" OnClick="btnToStore_OnClick" />
                            </td>
                        </tr>
                    </table>
                    <div  style="overflow: scroll; width:100%;">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="toptable grid" CellPadding="4" ForeColor="#333333" DataKeyNames="MP_CHPTCODE"
                        OnRowDataBound="GridView1_RowDataBound" OnDataBound="GridView1_DataBound" OnPageIndexChanging="GridView1_PageIndexChanging"
                        Width="100%" EmptyDataText="没有记录" PageSize="10">
                        <PagerTemplate>
                            <table width="100%" style="border: 0px;" align="center">
                                <tr>
                                    <td style="border-bottom-style: ridge; width: 100%; text-align: center">
                                        <asp:Label ID="lblCurrrentPage" runat="server" ForeColor="#CC3300"></asp:Label>
                                        <span>跳转至</span>
                                        <asp:DropDownList ID="page_DropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="page_DropDownList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <span>页</span>
                                        <asp:LinkButton ID="lnkBtnFirst" CommandArgument="First" CommandName="page" runat="server">第一页</asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnPrev" CommandArgument="prev" CommandName="page" runat="server">上一页</asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnNext" CommandArgument="Next" CommandName="page" runat="server">下一页</asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnLast" CommandArgument="Last" CommandName="page" runat="server">最后一页</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </PagerTemplate>
                        <Columns>
                               <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbSelect" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="行号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Eval("MP_ID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="变更批号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" Visible="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="chpcode" runat="server" Text='<%# Eval("MP_CHPCODE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="计划号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="chptcode" runat="server" Text='<%# Eval("MP_CHPTCODE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:BoundField DataField="MP_PROJNAME" HeaderStyle-Wrap="false" HeaderText="项目" SortExpression="MP_PROJNAME" ItemStyle-Wrap="False"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="TSA_ENGNAME" HeaderStyle-Wrap="false" HeaderText="工程" SortExpression="TSA_ENGNAME" ItemStyle-Wrap="False"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_TUHAO" HeaderStyle-Wrap="false" HeaderText="图号(标识号)" 
                                SortExpression="PUR_TUHAO" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="物料编码" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="marid" runat="server" Text='<%# Eval("MP_MARID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:BoundField DataField="MNAME" HeaderStyle-Wrap="false" HeaderText="名称" SortExpression="MNAME" ItemStyle-Wrap="False"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GUIGE" HeaderStyle-Wrap="false" HeaderText="规格" SortExpression="GUIGE" ItemStyle-Wrap="False"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="CAIZHI" HeaderStyle-Wrap="false" HeaderText="材质" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="GB" HeaderStyle-Wrap="false" HeaderText="国标" SortExpression="GB" ItemStyle-Wrap="False"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="备库状态" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="False">
                                <ItemTemplate>
                                    <asp:Label ID="state" ForeColor="Red" runat="server" Text='<%# Eval("MP_EXECSTATE").ToString()=="0"?"未执行":Eval("MP_EXECSTATE").ToString()=="1"?"待备库":Eval("MP_EXECSTATE").ToString()=="2"?"调整中":Eval("MP_EXECSTATE").ToString()=="3"?"已调整":"关闭" %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="变更数量" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="chnum" runat="server" Text='<%# Eval("MP_BGNUM") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:BoundField DataField="PURCUNIT" HeaderText="单位" HeaderStyle-Wrap="false" SortExpression="PURCUNIT" ItemStyle-Wrap="False"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="变更辅助数量" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="False">
                                <ItemTemplate>
                                    <asp:Label ID="chfznum" runat="server" Text='<%# Eval("MP_BGFZNUM") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:BoundField DataField="FUZHUUNIT" HeaderText="辅助单位" HeaderStyle-Wrap="false" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_BGZXNUM" HeaderText="执行数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_BGZXFZNUM" HeaderText="执行辅助数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_BKNUM" HeaderStyle-Wrap="false" HeaderText="备库数量" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_BKFZNUM" HeaderStyle-Wrap="false" HeaderText="备库辅助数量" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_SUBNAME" HeaderStyle-Wrap="false" HeaderText="备库提交人" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_SUBTIME" HeaderStyle-Wrap="false" HeaderText="提交时间" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_EXECNAME" HeaderStyle-Wrap="false" HeaderText="备库执行人" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_EXECTIME" HeaderStyle-Wrap="false" HeaderText="执行时间" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="MP_BKNOTE" HeaderStyle-Wrap="false" HeaderText="备注" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <br />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
    <ProgressTemplate>
       <div style="position: absolute; top: 40%; right:40%">
       <table>
       <tr>
       <td align="right"><asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" /></td>
       <td align="left" style="background-color:Yellow; font-size:medium;">数据处理中，请稍后...</td>
       </tr>
       </table>
       </div>
    </ProgressTemplate>
</asp:UpdateProgress>
</asp:Content>
