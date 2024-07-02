 

CREATE DEFINER=`sa`@`%` PROCEDURE `pharmacy_inventory`.`sp_sync_product_unit_stock_from_shipment`(
    IN p_shipment_id int,
    IN p_product_unit_id int,
    IN p_shipment_status varchar(100)
)
BEGIN
    /*
     * Created date : 2024/03/04
     * Author : Khoabd
     * Description: sync data from shipment to stock
     * */
    INSERT INTO tmp_log (cou, str) 
    VALUES(p_shipment_id, p_shipment_status),
            (p_product_unit_id, 'aaa');
    
    CREATE TEMPORARY TABLE temp_table (
        product_unit_id int ,
         quantity bigint
    );
    
    IF p_shipment_status = 'IMPORT' THEN 
        -- get quantity to update
        INSERT INTO temp_table(product_unit_id, quantity)
        SELECT 
            sd.product_unit_id
            ,IFNULL(ps.stock_quantity, 0) + sd.quantity AS quantity
        FROM 
            shipment_detail AS sd
        LEFT JOIN 
            product_stock AS ps
            ON ps.is_deleted = FALSE AND ps.is_current = TRUE
            AND sd.product_unit_id = ps.product_unit_id
        WHERE   
            sd.is_deleted = FALSE 
        AND 
            sd.shipment_id = p_shipment_id
        AND 
            sd.product_unit_id = p_product_unit_id;
        
        INSERT INTO tmp_log (cou, str)
        SELECT 
            tmp.quantity, 'bbb'
        FROM temp_table AS tmp;
        
         -- disable last stock data
        UPDATE pharmacy_inventory.product_stock
        SET 
            is_current = FALSE 
        WHERE
            product_stock.is_deleted  = FALSE 
        AND 
            product_stock.is_current = TRUE
        AND 
            product_stock.product_unit_id = p_product_unit_id;
    
        -- insert new stock data
        INSERT INTO pharmacy_inventory.product_stock(product_unit_id, stock_quantity, created_by, updated_by)
        SELECT 
            tmp.product_unit_id
            ,tmp.quantity
            ,0
            ,0
        FROM 
            temp_table AS tmp;
        
    ELSEIF p_shipment_status = 'EXPORT' THEN 
        -- get quantity to update
        INSERT INTO temp_table(product_unit_id, quantity)
        SELECT 
            sd.product_unit_id
            ,IFNULL(ps.stock_quantity, 0) - sd.quantity AS quantity
        FROM 
            shipment_detail AS sd
        LEFT JOIN 
            product_stock AS ps
            ON ps.is_deleted = FALSE AND ps.is_current = TRUE
            AND sd.product_unit_id = ps.product_unit_id
        WHERE   
            sd.is_deleted = FALSE 
        AND 
            sd.shipment_id = p_shipment_id
        AND 
            sd.product_unit_id = p_product_unit_id;
        
        -- disable last stock data
        UPDATE pharmacy_inventory.product_stock
        SET 
            is_current = FALSE 
        WHERE
            product_stock.is_deleted  = FALSE 
        AND 
            product_stock.is_current = TRUE
        AND 
            product_stock.product_unit_id = p_product_unit_id;
        
        -- insert new stock data
        INSERT INTO pharmacy_inventory.product_stock(product_unit_id, stock_quantity, created_by, updated_by)
        SELECT 
            tmp.product_unit_id
            ,tmp.quantity
            ,0
            ,0
        FROM 
           temp_table AS tmp;
    END IF;

    DROP TEMPORARY TABLE IF EXISTS temp_table;
END

-- trigger for sync product stock when have data - 2024-03-04 - khoabd
DELIMITER //
CREATE TRIGGER sync_product_unit_stock_trigger
AFTER INSERT ON shipment_detail
FOR EACH ROW
BEGIN
    DECLARE shipment_status varchar(50);

    SELECT 
        s.shipment_status INTO shipment_status
    FROM 
        shipment AS s
    WHERE 
        s.is_deleted = FALSE 
    AND 
        s.shipment_id = NEW.shipment_id;
    
    CALL pharmacy_inventory.sp_sync_product_unit_stock_from_shipment(NEW.shipment_id, NEW.product_unit_id, shipment_status);
END;
//
DELIMITER ;
-- ====================================================================================================================================

-- trigger for sync information of product with medicine if product is a medicine - 2024-03-04 - khoabd
DELIMITER //
CREATE TRIGGER aft_insert_product_trigger
AFTER INSERT ON product
FOR EACH ROW
BEGIN
    IF NEW.medicine_id IS NOT NULL THEN 
        UPDATE product 
        JOIN medicine 
            ON medicine.medicine_id = NEW.medicine_id
            AND medicine.is_deleted = FALSE 
        SET 
            product.packing_size = m.packing_size ,
            product.manufacturer_id =  medicine.manufacturer_id ;
    END IF ;
END;
//
DELIMITER ;










