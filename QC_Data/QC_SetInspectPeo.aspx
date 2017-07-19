<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="QC_SetInspectPeo.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_SetInspectPeo" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    质检人员设置
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <script type="text/javascript">
        //初始化选人窗口
        $(function() {
            $('#win').show().dialog({
                title: '修改质检信息',
                width: 270,
                height: 240,
                closed: true,
                cache: false,
                modal: true,
                buttons: '#buttons'
            });
        });
        //初始化编辑表单
        $(function() {
            $('#frmEdit').form({
            url: "QC_AjaxHandler.aspx?method=SetInspectPer",
             
                success: function(data) {
                    alert(data)
                }
            });
        });

        $(function() {
            console.log($("#<%= GridView1.ClientID %> a"));
            $("#<%= GridView1.ClientID %> a").click(function() {
                // console.log($(this).parent().parent().find("td"));
                $("#win").dialog('open');
                //  console.log($(this).parent().parent().find("td:first input:first").val());
                var num = $(this).parent().parent().find("td:first input:first").val();
                var name = $(this).parent().parent().find("td:first input:eq(1)").val();
                var per = $(this).parent().parent().find("td:first input:eq(2)").val();
                var isDiret = $(this).parent().parent().find("td:first input:last").val();
                $("#txtWlCode").val(num);
                $("#txtName").val(name);
                $("#txtInspectPer").combobox('setValue', per);
                $("#txtIsDirect").combobox('setValue', isDiret);

            });
        });

        function submitForm() {
           // console.log($("#txtInspectPer").combobox('getValue'));
            if ($("#txtInspectPer").combobox('getValue') != null) {
                $.ajax({
                    type: "POST",
                    url: "QC_AjaxHandler.aspx",
                    data: { method: "SetInspectPer", num: $("#txtWlCode").val(), per: $("#txtInspectPer").combobox('getValue'), isDiret: $("#txtIsDirect").combobox('getValue') },
                    success: function(msg) {
                    if (msg) {
                        alert("保存成功，请刷新页面！");
                        $("#win").dialog('close');
                         };
                    }

                });
            
            }
        }
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%; height: 24px">
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div class="box-outer" style="width: 99%; overflow: auto;">
                    <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                    <input id="Hidden2" type="hidden" value='<%# Eval("num") %>' runat="server" />
                                    <input id="Hidden3" type="hidden" value='<%# Eval("TY_NAME") %>' runat="server" />
                                    <input id="Hidden1" type="hidden" value='<%# Eval("InspectPerson") %>' runat="server" />
                                    <input id="Hidden4" type="hidden" value='<%# Eval("IsDiret") %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="num" HeaderText="物料编码" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TY_NAME" HeaderText="名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ST_NAME" HeaderText="质检人员" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Addtime" HeaderText="维护时间" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="AddName" HeaderText="维护人" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="是否二次分配" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsDiret" runat="server" Text='<%# Eval("IsDiret").ToString()=="0"?"是":Eval("IsDiret").ToString()=="1"?"否":"未设置" %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="修改" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hytask" runat="server">
                                        <asp:Image ID="imgtask" ImageUrl="~/assets/images/res.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />
                                        修改</asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <asp:Panel ID="NoDataPanel" runat="server">
                        没有记录!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
        <div id="win">
            <form  id="frmEdit">
            <table>
                <tr>
                    <td>
                        物料编码:
                    </td>
                    <td>
                        <input type="text" id="txtWlCode" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td>
                        名称:
                    </td>
                    <td>
                        <input type="text" id="txtName" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td>
                        质检人员::
                    </td>
                    <td>
               <input id="txtInspectPer" class="easyui-combobox" data-options="valueField:'st_id',textField:'st_name',url:'QC_AjaxHandler.aspx?method=GetAllTechPer'" />
                    </td>
                </tr>
                <tr>
                    <td>
                        是否二次分配:
                    </td>
                    <td>
                        <select id="txtIsDirect" class="easyui-combobox" name="IsDirect" data-options="panelHeight:'auto'">
                            <option value="0">是</option>
                            <option value="1">否</option>
                        </select>
                    </td>
                </tr>
                <tr><td colspan="2"><span style="color:Red">注:如果该质检项为二次分配，则指定的质检人无效</span></td></tr>
            </table>
            </form>
        </div>
        <div id="buttons" style="text-align: right" visible="false">
            <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="submitForm()">
                保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                    onclick="javascript:$('#win').dialog('close')">取消</a>
        </div>
    </div>
</asp:Content>
