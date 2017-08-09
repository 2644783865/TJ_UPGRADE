<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master"
    CodeBehind="YS_Cost_Budget_O_M.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.YS_Cost_Budget_O_M" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    订单监控/分析
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>

    <script type="text/javascript" language="javascript">
     var selectedColor = "#C0FF3E";
     var rowOverColor = "blue";
     var rowColor = "#EFF3FB";
     var selectedRows = new Object();
    
    function SelectRow(uniqueId, element)
     {
        if (typeof(selectedRows[uniqueId]) == "undefined")
            selectedRows[uniqueId] = false;
        selectedRows[uniqueId] = !selectedRows[uniqueId];
        element.style.backgroundColor = selectedRows[uniqueId] ? selectedColor : rowColor;
     }
     
     function ShowContract(id) {
            var autonum = Math.round(10000 * Math.random());
            window.open("../Contract_Data/CM_Contract_SW_Add.aspx?Action=View&autonum=" + autonum + "&condetail_id=" + id);
        }
        
        function PurMarView_Amount(ContractNo, FatherCode) {
             var autonum = Math.round(10000 * Math.random());
         window.open("../PC_Data/TBPC_Order_Amount_detail.aspx?ContractNo=" + ContractNo + "");
        }
        
     function PurMarView(ContractNo, FatherCode) {
     var autonum = Math.round(10000 * Math.random());
    window.open("YS_Cost_Budget_O_M_Detail.aspx?ContractNo=" + ContractNo + "&FatherCode=" + FatherCode + "");
        }
        
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td>
                            任务号：<asp:TextBox ID="txt_search" runat="server" Text="" Width="150px">
                            </asp:TextBox><asp:Button ID="search1" runat="server" Text="查 询" OnClick="btn_search_OnClick" />
                        </td>
                        <td>
                            项目名称:
                            <asp:DropDownList ID="ddl_project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 工程名称:
                            <asp:DropDownList ID="ddl_engineer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                            </asp:DropDownList>
                        </td>
                        <td>
                        <asp:Button ID="Btn_update" runat="server" Text="订单费用更新" OnClick="Btn_update_OnClick" />
                        </td>               
                        <td align="right">
                            <asp:Button ID="btn_daochu" runat="server" Text="导出Excel" OnClick="btn_daochu_Click" />&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="width: 100%; overflow: auto; overflow-x: yes; overflow-y: hidden;">
                <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid nowrap" runat="server"
                    AutoGenerateColumns="False" CellPadding="1" ForeColor="#333333" OnRowDataBound="GridView1_onrowdatabound">
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" HeaderText="序号"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="合同号" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_YS_CONTRACT_NO" runat="server" Text='<%#Eval("YS_CONTRACT_NO") %>'
                                    ToolTip="双击关联合同信息！"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="任务号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pcon_sch" runat="server" Text='<%#Eval("PCON_SCH") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="项目名称" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pcon_pjname" runat="server" Text='<%#Eval("PCON_PJNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="工程名称" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pcon_engname" runat="server" Text='<%#Eval("PCON_ENGNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="材料预算总额" HeaderStyle-ForeColor="#363636"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_MAR_AMOUNT" runat="Server" Value='<%#Eval("YS_MAR_AMOUNT_B_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                    <asp:Label ID="lab_MAR_AMOUNT" runat="Server" Text='<%#Eval("YS_MAR_AMOUNT_B_BG","{0:N2}") %>' />
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Convert.ToDouble(Eval("YS_MAR_AMOUNT_B_BG_percent"))) %>"
                                        height="9" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                      <%--  <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="技术外协(预)" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_OUT_LAB_MAR" runat="Server" Value='<%#Eval("YS_OUT_LAB_MAR_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_OUT_LAB_MAR" runat="Server" Text='<%#Eval("YS_OUT_LAB_MAR_BG","{0:N2}") %>' />
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Convert.ToDouble(Eval("YS_OUT_LAB_MAR_BG_percent"))) %>"
                                        height="9" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="黑色金属(预)" HeaderStyle-ForeColor="#363636"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_FERROUS_METAL" runat="Server" Value='<%#Eval("YS_FERROUS_METAL_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_FERROUS_METAL" runat="Server" Text='<%#Eval("YS_FERROUS_METAL_BG","{0:N2}") %>' />
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Convert.ToDouble(Eval("YS_FERROUS_METAL_BG_percent"))) %>"
                                        height="9" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="外购件(预)" HeaderStyle-ForeColor="#363636"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_PURCHASE_PART" runat="Server" Value='<%#Eval("YS_PURCHASE_PART_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_PURCHASE_PART" runat="Server" Text='<%#Eval("YS_PURCHASE_PART_BG","{0:N2}") %>' />
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Convert.ToDouble(Eval("YS_PURCHASE_PART_BG_percent"))) %>"
                                        height="9" id="ID_IMG" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="加工件(预)" HeaderStyle-ForeColor="#363636"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_MACHINING_PART" runat="Server" Value='<%#Eval("YS_MACHINING_PART_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_MACHINING_PART" runat="server" Text='<%#Eval("YS_MACHINING_PART_BG","{0:N2}") %>'></asp:Label>
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Convert.ToDouble(Eval("YS_MACHINING_PART_BG_percent"))) %>"
                                        height="9" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="油漆涂料(预)" HeaderStyle-ForeColor="#363636"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_PAINT_COATING" runat="Server" Value='<%#Eval("YS_PAINT_COATING_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_PAINT_COATING" runat="server" Text='<%#Eval("YS_PAINT_COATING_BG","{0:N2}") %>'></asp:Label>
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Convert.ToDouble(Eval("YS_PAINT_COATING_BG_percent"))) %>"
                                        height="9" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="电气电料(预)" HeaderStyle-ForeColor="#363636"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_ELECTRICAL" runat="Server" Value='<%#Eval("YS_ELECTRICAL_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_ELECTRICAL" runat="server" Text='<%#Eval("YS_ELECTRICAL_BG","{0:N2}") %>'></asp:Label>
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Convert.ToDouble(Eval("YS_ELECTRICAL_BG_percent"))) %>"
                                        height="9" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="其它材料费(预)" HeaderStyle-ForeColor="#363636"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_OTHERMAT_COST" runat="Server" Value='<%#Eval("YS_OTHERMAT_COST_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_OTHERMAT_COST" runat="server" Text='<%#Eval("YS_OTHERMAT_COST_BG","{0:N2}") %>'></asp:Label>
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Convert.ToDouble(Eval("YS_OTHERMAT_COST_BG_percent"))) %>"
                                        height="9" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="材料订单总额" HeaderStyle-ForeColor="#8E388E"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lab_MAR_AMOUNT_O" runat="Server" Text='<%#Eval("YS_MAR_AMOUNT_O_BG","{0:N2}") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <%--<asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="技术外协(订)" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                            <asp:Label ID="lab_OUT_LAB_MAR_O" runat="server" Text='<%#Eval("YS_OUT_LAB_MAR","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="黑色金属(订)" HeaderStyle-ForeColor="#8E388E"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                            <asp:Label ID="lab_FERROUS_METAL_O" runat="server" Text='<%#Eval("YS_FERROUS_METAL","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="外购件(订)" HeaderStyle-ForeColor="#8E388E"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                            <asp:Label ID="lab_PURCHASE_PART_O" runat="server" Text='<%#Eval("YS_PURCHASE_PART","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="加工件(订)" HeaderStyle-ForeColor="#8E388E"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                            <asp:Label ID="lab_MACHINING_PART_O" runat="server" Text='<%#Eval("YS_MACHINING_PART","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="油漆涂料(订)" HeaderStyle-ForeColor="#8E388E"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                            <asp:Label ID="lab_PAINT_COATING_O" runat="server" Text='<%#Eval("YS_PAINT_COATING","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="电气电料(订)" HeaderStyle-ForeColor="#8E388E"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                            <asp:Label ID="lab_ELECTRICAL_O" runat="server" Text='<%#Eval("YS_ELECTRICAL","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="其他材料费(订)" HeaderStyle-ForeColor="#8E388E"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                            <asp:Label ID="lab_OTHERMAT_COST_O" runat="server" Text='<%#Eval("YS_OTHERMAT_COST","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%-- <asp:BoundField DataField="YS_OUT_LAB_MAR" HeaderText="技术外协(订)" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-ForeColor="#660066" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_FERROUS_METAL" HeaderText="黑色金属(订)" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-ForeColor="#000066" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_PURCHASE_PART" HeaderText="外购件(订)" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-ForeColor="#000066" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_MACHINING_PART" HeaderText="加工件(订)" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-ForeColor="#000066" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_PAINT_COATING" HeaderText="油漆涂料(订)" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-ForeColor="#000066" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_ELECTRICAL" HeaderText="电气电料(订)" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-ForeColor="#000066" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_OTHERMAT_COST" HeaderText="其它材料费(订)" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-ForeColor="#000066" HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />--%>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                </br>
            </div>
            <asp:Panel ID="NoDataPanel" runat="server">
                没有记录!</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
    <div>
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td style="width: 15" align="Left">
                    <img alt="" src="/YS_Data/update1.jpg" width="50px" height="15" />
                    <asp:Label ID="labe1" runat="Server" Text="表示订单完成百分比" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                    &nbsp;&nbsp;&nbsp;
                    <img alt="" src="/YS_Data/LightSalmon.jpg" width="50px" height="15" />
                    <asp:Label ID="Label3" runat="Server" Text="表示订单达到预算90%" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                    &nbsp;&nbsp;&nbsp;
                    <img alt="" src="/YS_Data/Yellow.jpg" width="50px" height="15" />
                    <asp:Label ID="Label2" runat="Server" Text="表示订单超过预算" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

